using System;

namespace CurrencyGrpcService.Conversion
{
    /// <summary>
    /// Interface to convert numbers
    /// </summary>
    public interface INumberConverter
    {
        /// <summary>
        /// Convert an integer to words for a given locale
        /// </summary>
        /// <param name="n">Integer to convert</param>
        /// <param name="locale">Locale to use</param>
        /// <returns>Words corresponding to the given integer</returns>
        /// <exception cref="ArgumentException">If the number is out of range, or the locale does not contains special values for this number</exception>
        public string ConvertToWords(int n, ILocale locale);

        /// <summary>
        /// Message to return if out of range
        /// </summary>
        /// <returns>Message if out of range</returns>
        public string GetOutOfRangeMessage();
    }
}