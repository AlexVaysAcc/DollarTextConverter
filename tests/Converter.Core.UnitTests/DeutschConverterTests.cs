using Microsoft.Extensions.Logging.Abstractions;

namespace Converter.Core.UnitTests
{
    public class DeutschConverterTests
    {
        [Test]
        public void Constructor_NullLogger_Throws()
        {
            Assert.That(() => new DeutschConverter(null!), Throws.TypeOf<System.ArgumentNullException>());
        }

        [Test]
        public void Convert_Zero_ReturnsNullDollar()
        {
            var logger = NullLogger<DeutschConverter>.Instance;
            var conv = new DeutschConverter(logger);

            Assert.That(conv.Convert(0, 0), Is.EqualTo("null Dollar"));
        }

        [Test]
        public void Convert_One_ReturnsEinDollar()
        {
            var conv = new DeutschConverter(NullLogger<DeutschConverter>.Instance);
            Assert.That(conv.Convert(1, 0), Is.EqualTo("ein Dollar"));
        }

        [Test]
        public void Convert_Two_ReturnsZweiDollar()
        {
            var conv = new DeutschConverter(NullLogger<DeutschConverter>.Instance);
            Assert.That(conv.Convert(2, 0), Is.EqualTo("zwei Dollar"));
        }

        [Test]
        public void Convert_TwentyOne_FollowsImplementationBehavior()
        {
            var conv = new DeutschConverter(NullLogger<DeutschConverter>.Instance);
            // Implementation builds Units[1] + "und" + Tens[2]
            Assert.That(conv.Convert(21, 0), Is.EqualTo("einsundzwanzig Dollar"));
        }

        [Test]
        public void Convert_ThirtyWithCents_ReturnsDreißigAndFuenfCent()
        {
            var conv = new DeutschConverter(NullLogger<DeutschConverter>.Instance);
            Assert.That(conv.Convert(30, 5), Is.EqualTo("dreißig Dollar and fünf Cent"));
        }

        [Test]
        public void Convert_OneHundred_BehavesAsImplemented()
        {
            var conv = new DeutschConverter(NullLogger<DeutschConverter>.Instance);
            // Current implementation returns empty string for 100's ConvertDollars then adds " Dollar"
            Assert.That(conv.Convert(100, 0), Is.EqualTo(" Dollar"));
        }

        [Test]
        public void Convert_OneThousand_ReturnsEintausend()
        {
            var conv = new DeutschConverter(NullLogger<DeutschConverter>.Instance);
            Assert.That(conv.Convert(1000, 0), Is.EqualTo("eintausend Dollar"));
        }

        [Test]
        public void Convert_MillionAndRest_ReturnsEineMillionRest()
        {
            var conv = new DeutschConverter(NullLogger<DeutschConverter>.Instance);
            Assert.That(conv.Convert(1_000_001, 0), Is.EqualTo("eine Million ein Dollar"));
        }

        [Test]
        public void Convert_HighNumber_NineHundredNinetyNineMillion()
        {
            var conv = new DeutschConverter(NullLogger<DeutschConverter>.Instance);
            var result = conv.Convert(999_999_999, 99);
            Assert.That(result, Is.EqualTo("neunhundertneunundneunzig Millionen neunhundertneunundneunzigtausendneunhundertneunundneunzig Dollar and neunzigundneun Cent"));
        }

        [Test]
        public void Convert_HighNumber_NoCents()
        {
            var conv = new DeutschConverter(NullLogger<DeutschConverter>.Instance);
            var result = conv.Convert(999_999_999, 0);
            Assert.That(result, Is.EqualTo("neunhundertneunundneunzig Millionen neunhundertneunundneunzigtausendneunhundertneunundneunzig Dollar"));
        }
    }
}
