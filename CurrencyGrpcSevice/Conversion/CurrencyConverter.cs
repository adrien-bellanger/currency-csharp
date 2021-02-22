using System;

namespace CurrencyGrpcService.Conversion
{
    /// <summary>
    /// The only implementation of the currency converter in this project.
    /// </summary>
    public class CurrencyConverter : ICurrencyConverter
    {
        /// <summary>
        /// Constructor for the currency converter
        /// </summary>
        /// <param name="numberConverter">Number converter to use for this currency converter</param>
        public CurrencyConverter(INumberConverter numberConverter)
        {
            NumberConverter = numberConverter;
        }
        
        /// <summary>
        /// Convert a decimalfor a given currency and locale to words
        /// </summary>
        /// <param name="n">Decimal to convert</param>
        /// <param name="currencyName">Currency name</param>
        /// <param name="locale">Locale to use</param>
        /// <returns>Words corresponding to the given double and currency</returns>
        public string ConvertToWords(decimal d, ICurrency currency, ILocale locale)
        {
            // If d is not convertible to an int it is out of range
            if (d > int.MaxValue || d < int.MinValue)
                return NumberConverter.GetOutOfRangeMessage();

            try
            {
                // Calculate the number of units (dollars) in d
                int nUnits = decimal.ToInt32(Math.Floor(d));
                // Convert the number of units (dollars) to words, including the unit's name
                string sUnits = ConvertToWords(nUnits, currency.UnitName, locale);

                // Calculate the number of cents in d
                int nCents = decimal.ToInt32((d - nUnits) * 100);
                // If d does not contain any cents, return the words for the units.
                if (nCents == 0)
                    return sUnits;

                // Convert the number of cents to words, including the cent's name
                string sCents = ConvertToWords(nCents, currency.CentName, locale);

                // Concatenate the units' words and cents'words with the corresponding connector inbetween
                return $"{sUnits} {locale.GetConnectorBetweenUnitAndCent()} {sCents}";
            }
            catch (ArgumentException e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Convert an double for a given currency and locale to words
        /// </summary>
        /// <param name="n">Double to convert</param>
        /// <param name="currencyName">Currency name</param>
        /// <param name="locale">Locale to use</param>
        /// <returns>Words corresponding to the given double and currency</returns>
        public string ConvertToWords(double dAsDouble, ICurrency currency, ILocale locale)
        {
            // To compensate the lack of precision of double, it will be rounded to the cent and then used only as decimal
            decimal d = Math.Round(new decimal(dAsDouble), 2);

            // Call ConvertToWords with the rounded-decimal-value
            return ConvertToWords(d, currency, locale);
        }

        /// <summary>
        /// Convert an integer for a given currency name and locale to words
        /// </summary>
        /// <param name="n">Integer to convert</param>
        /// <param name="currencyName">Currency name</param>
        /// <param name="locale">Locale to use</param>
        /// <returns>Words corresponding to the given integer and currency name</returns>
        private string ConvertToWords(int n, ICurrency.IWord currencyName, ILocale locale)
        {
            // Convert the number to words
            string sResultNumber = NumberConverter.ConvertToWords(n, locale);
            // Get the unit name
            string sUnitName = (n == 1) ? currencyName.Singular : currencyName.Plural;

            // Concatenate number and unit name
            return $"{sResultNumber} {sUnitName}";
        }

        /// <summary>
        /// Number converter to use for this class
        /// </summary>
        private INumberConverter NumberConverter { get; set; }
    }
}
