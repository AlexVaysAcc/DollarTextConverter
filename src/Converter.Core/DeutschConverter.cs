
using Microsoft.Extensions.Logging;

namespace Converter.Core
{
    public class DeutschConverter : IConverter
    {
        private readonly ILogger<DeutschConverter> mLogger;
        private static readonly string[] Units = { "", "eins", "zwei", "drei", "vier", "fünf", "sechs", "sieben", "acht", "neun", "zehn", "elf", "zwölf", "dreizehn", "vierzehn", "fünfzehn", "sechzehn", "siebzehn", "achtzehn", "neunzehn" };
        private static readonly string[] Tens = { "", "", "zwanzig", "dreißig", "vierzig", "fünfzig", "sechzig", "siebzig", "achtzig", "neunzig" };

        public DeutschConverter(ILogger<DeutschConverter> logger)
        {
            mLogger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string Convert(long dollars, int cents)
        {
            mLogger.LogInformation($"Converting currency amount to text for {dollars} dollars and {cents} cents.", dollars, cents);

            string dollarsText = dollars == 0 ? "null Dollar" : $"{ConvertDollars(dollars)} Dollar";

            string centsText = cents == 0 ? "" : $"{ConvertCents(cents)} Cent";

            string resultText = cents == 0 ? dollarsText : $"{dollarsText} and {centsText}";

            mLogger.LogDebug("Conversion result: {ResultText}", resultText);

            return resultText;
        }

        private string ConvertDollars(long dollars)
        {

            if (dollars == 1) return "ein";

            if (dollars < 20) return Units[dollars];

            if (dollars < 100) return dollars % 10 == 0 ? Tens[dollars / 10] : Units[dollars % 10] + "und" + (dollars / 10 > 0 ? Tens[dollars / 10] : "");

            if (dollars < 200) return dollars % 10 == 0 ? Tens[dollars / 100] : "einhundert" + (dollars % 100 > 0 ? ConvertDollars(dollars % 100) : "");

            if (dollars < 1000) return Units[dollars / 100] + "hundert" + (dollars % 100 > 0 ? ConvertDollars(dollars % 100) : "");

            if (dollars < 1000000) return ConvertDollars(dollars/1000) + "tausend" + (dollars % 1000 > 0 ? ConvertDollars(dollars % 1000) : "");

            if (dollars < 2000000) return "eine Million " + (dollars % 1000000 > 0 ? ConvertDollars(dollars % 1000000) : "");

            return ConvertDollars(dollars / 1000000) + " Millionen " + (dollars % 1000000 > 0 ? ConvertDollars(dollars % 1000000) : "");
        }

        private string ConvertCents(int cents)
        {
            if (cents == 1) return "ein";
            if (cents < 20) return Units[cents];
            return cents % 10 == 0 ? Tens[cents / 10] : Tens[cents % 10] + "und" + (cents % 10 > 0 ? Units[cents / 10] : "");
        }
    }
}
