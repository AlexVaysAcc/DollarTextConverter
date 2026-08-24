using Microsoft.Extensions.Logging;

namespace Converter.Core
{
    public class EnglishConverter : IConverter

    {
        private readonly ILogger<EnglishConverter> mLogger;

        private static readonly string[] Units = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", 
            "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

        private static readonly string[] Tens = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        private static readonly string[] HighNumbers = { "", "thousand", "million", };

        public EnglishConverter(ILogger<EnglishConverter> logger)
        {
            mLogger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public string Convert(long dollars, int cents)
        {
            mLogger.LogInformation($"Converting currency amount to text for {dollars} dollars and {cents} cents.", dollars, cents);

            string dollarsText = dollars == 0 ? "zero dollars" : $"{ConvertDollars(dollars)} {(dollars == 1 ? "dollar" : "dollars")}";

            string centsText = cents == 0 ? "" : $"{ConvertCents(cents)} {(cents == 1 ? "cent" : "cents")}";

            string resultText = cents == 0 ? dollarsText : $"{dollarsText} and {centsText}";

            mLogger.LogDebug("Conversion result: {ResultText}", resultText);

            return resultText;
        }


        private string ConvertDollars(long dollars)
        {

            if (dollars == 0) return "";

            if (dollars < 20) return Units[dollars];

            if (dollars < 100) return dollars % 10 == 0 ? Tens[dollars / 10] : Tens[dollars / 10] + "-" + (dollars % 10 > 0 ? Units[dollars % 10] : "");

            if (dollars < 1000) return Units[dollars / 100] + " hundred" + (dollars % 100 > 0 ? " " + ConvertDollars(dollars % 100) : "");


            for (int i = 2; i > 0; i--)
            {
                long millenary = (long)Math.Pow(1000, i);

                if (dollars >= millenary)
                {
                    return ConvertDollars(dollars / millenary) + " " + HighNumbers[i] + (dollars % millenary > 0 ? " " + ConvertDollars(dollars % millenary) : "");
                }
            }

            return "";
        }

        private string ConvertCents(int cents)
        {
            if (cents < 20) return Units[cents];
            return cents % 10 == 0 ? Tens[cents / 10] : Tens[cents / 10] + "-" + (cents % 10 > 0 ? Units[cents % 10] : "");
        }
    }
}
