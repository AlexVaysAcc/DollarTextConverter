using Converter.API;
using Converter.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace Server.UnitTests;

[TestFixture]
public class ConverterFactoryTests
{
    private ConverterFactory mFactory;
    private Mock<ILogger<ConverterFactory>> mLoggerMock;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection();

        services.AddLogging();

        services.AddTransient<EnglishConverter>();
        services.AddTransient<DeutschConverter>();

        var serviceProvider = services.BuildServiceProvider();

        mLoggerMock = new Mock<ILogger<ConverterFactory>>();

        mFactory = new ConverterFactory(serviceProvider, mLoggerMock.Object);
    }

    [TestCase("en", typeof(EnglishConverter))]
    [TestCase("de", typeof(DeutschConverter))]
    public void GetConverter_ValidLanguage_ResolvesCorrectTypeFromDI(string language, Type expectedType)
    {
        // Act
        IConverter result = mFactory.GetConverter(language);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf(expectedType));
    }

    [Test]
    public void GetConverter_UnsupportedLanguage_ThrowsNotSupportedException()
    {
        // Act & Assert
        var exception = Assert.Throws<NotSupportedException>(() => mFactory.GetConverter("xyz"));
        Assert.That(exception.Message, Does.Contain("not supported"));
    }
}