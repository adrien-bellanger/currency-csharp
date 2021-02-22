namespace CurrencyGrpcService.Conversion
{
    /// <summary>
    /// Interface for the currency converters
    /// </summary>
    public interface ICurrencyConverter
    {
        /// <summary>
        /// Convert a decimal for a given currency and locale to words
        /// </summary>
        /// <param name="d">Decimal to convert</param>
        /// <param name="currency">Currency name</param>
        /// <param name="locale">Locale to use</param>
        /// <returns>Words corresponding to the given double and currency</returns>
        public string ConvertToWords(decimal d, ICurrency currency, ILocale locale);
        /// <summary>
        /// Convert an double for a given currency and locale to words
        /// </summary>
        /// <param name="d">Double to convert</param>
        /// <param name="currency">Currency name</param>
        /// <param name="locale">Locale to use</param>
        /// <returns>Words corresponding to the given double and currency</returns>
        public string ConvertToWords(double d, ICurrency currency, ILocale locale);
    }
}
