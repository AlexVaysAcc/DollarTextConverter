using Converter.Core;

namespace Converter.API
{
    public class ConverterFactory
    {
        private readonly IServiceProvider mServiceProvider;
        private readonly ILogger<ConverterFactory> mLogger;

        public ConverterFactory(IServiceProvider serviceProvider, ILogger<ConverterFactory> logger)
        {
            mServiceProvider = serviceProvider;
            mLogger = logger;
        }

        public IConverter GetConverter(string language) 
        {

            mLogger.LogDebug("Resolving converter instance for language: {Language}", language);

            return language.ToLower()
                switch
            {
                "en" => mServiceProvider.GetRequiredService<EnglishConverter>(),
                "de" => mServiceProvider.GetRequiredService<DeutschConverter>(),
                _ => throw new NotSupportedException($"Language '{language}' is not supported")
            };
        }
    }
}
