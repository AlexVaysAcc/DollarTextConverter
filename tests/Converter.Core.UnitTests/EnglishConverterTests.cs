using Microsoft.Extensions.Logging.Abstractions;

namespace Converter.Core.UnitTests
{
    public class EnglishConverterTests
    {
        [Test]
        public void Constructor_NullLogger_Throws()
        {
            Assert.That(() => new EnglishConverter(null!), Throws.TypeOf<System.ArgumentNullException>());
        }

        [Test]
        public void Convert_ZeroDollarsZeroCents_ReturnsZeroDollars()
        {
            var logger = NullLogger<EnglishConverter>.Instance;
            var conv = new EnglishConverter(logger);

            var result = conv.Convert(0, 0);

            Assert.That(result, Is.EqualTo("zero dollars"));
        }

        [TestCase(1, 0, "one dollar")]
        [TestCase(2, 0, "two dollars")]
        [TestCase(21, 0, "twenty-one dollars")]
        [TestCase(342, 0, "three hundred forty-two dollars")]
        public void Convert_DollarsOnly_FormatsCorrectly(long dollars, int cents, string expected)
        {
            var conv = new EnglishConverter(NullLogger<EnglishConverter>.Instance);
            Assert.That(conv.Convert(dollars, cents), Is.EqualTo(expected));
        }

        [Test]
        public void Convert_CentsOnly_IncludesZeroDollarsAndCents()
        {
            var conv = new EnglishConverter(NullLogger<EnglishConverter>.Instance);
            var result = conv.Convert(0, 5);
            Assert.That(result, Is.EqualTo("zero dollars and five cents"));
        }

        [Test]
        public void Convert_SingularCent_UsesCentSingular()
        {
            var conv = new EnglishConverter(NullLogger<EnglishConverter>.Instance);
            var result = conv.Convert(0, 1);
            Assert.That(result, Is.EqualTo("zero dollars and one cent"));
        }

        [Test]
        public void Convert_Millions_And_ComplexNumber()
        {
            var conv = new EnglishConverter(NullLogger<EnglishConverter>.Instance);
            var result = conv.Convert(1_234_567, 89);
            Assert.That(result, Is.EqualTo("one million two hundred thirty-four thousand five hundred sixty-seven dollars and eighty-nine cents"));
        }

        [Test]
        public void Convert_TwoMillion_ReturnsPluralMillions()
        {
            var conv = new EnglishConverter(NullLogger<EnglishConverter>.Instance);
            var result = conv.Convert(2_000_000, 0);
            Assert.That(result, Is.EqualTo("two million dollars"));
        }

        [TestCase(20, 0, "twenty dollars")]
        [TestCase(30, 5, "thirty dollars and five cents")]
        [TestCase(99, 0, "ninety-nine dollars")]
        [TestCase(100, 0, "one hundred dollars")]
        [TestCase(101, 1, "one hundred one dollars and one cent")]
        [TestCase(1000, 0, "one thousand dollars")]
        [TestCase(1001, 0, "one thousand one dollars")]
        [TestCase(0, 50, "zero dollars and fifty cents")]
        [TestCase(0, 99, "zero dollars and ninety-nine cents")]
        public void Convert_MoreCases(long dollars, int cents, string expected)
        {
            var conv = new EnglishConverter(NullLogger<EnglishConverter>.Instance);
            Assert.That(conv.Convert(dollars, cents), Is.EqualTo(expected));
        }

        [Test]
        public void Convert_HighNumber_NineHundredNinetyNineMillion()
        {
            var conv = new EnglishConverter(NullLogger<EnglishConverter>.Instance);
            var result = conv.Convert(999_999_999, 99);
            Assert.That(result, Is.EqualTo("nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents"));
        }

        [Test]
        public void Convert_HighNumber_NoCents()
        {
            var conv = new EnglishConverter(NullLogger<EnglishConverter>.Instance);
            var result = conv.Convert(999_999_999, 0);
            Assert.That(result, Is.EqualTo("nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars"));
        }
    }
}
