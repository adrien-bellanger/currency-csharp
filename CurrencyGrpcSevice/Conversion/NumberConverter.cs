using System;

namespace CurrencyGrpcService.Conversion
{
    /// <summary>
    /// Only implementation of the number converter in this project
    /// </summary>
    public class NumberConverter : INumberConverter
    {
        public string ConvertToWords(int n, ILocale locale)
        {
            // If the integer is out of range, throw an argument exception
            if (n < _minimumValue || n > _maximumValue)
                throw new ArgumentException(GetOutOfRangeMessage());

            // If n is under 100, no prefix is written before the special value
            bool bNIsSmaller100 = (n < 100);
            if (bNIsSmaller100)
            {
                string sForN = locale.GetWordForSpecialValue(n);
                if (sForN != "")
                    return sForN;
            }

            // Find the greatest special value smaller or equal to n
            foreach (int nSpecialValue in locale.GetAllSpecialValuesInDescendingOrder())
            {
                if (n >= nSpecialValue)
                {
                    // Compute how many times the special value divide the integer to convert
                    int nPreSpecialValue = Decimal.ToInt32(Math.Floor(new decimal(n / nSpecialValue)));
                    // If n is smaller than 100, the string before the special value is empty, else it calls recusively ConvertToString of nPreSpecialValue
                    string sPreSpecialValue = bNIsSmaller100 ? "" : $"{ConvertToWords(nPreSpecialValue, locale)} ";

                    // Compute the rest after using the special value
                    int nRest = n % nSpecialValue;
                    // If the rest is 0, the string after the special value is empty, else it calls recursively ConvertToString of nRest
                    string sPostSpecialValue = (nRest == 0) ? "" : ConvertToWords(nRest, locale);

                    // Concatenate those strings to obtain the words corresponding to n
                    return $"{sPreSpecialValue}{locale.GetWordForSpecialValue(nSpecialValue)}{locale.GetConnectorAfterSpecialValue(n, nSpecialValue)}{sPostSpecialValue}";
                }
            }

            // Some special values are missing in the given locale
            throw new ArgumentException($"The given locale is missing some special value to convert {n} to words");
        }

        public string GetOutOfRangeMessage()
        {
            return $"The number to convert has to be between {_minimumValue} and {_maximumValue}.";
        }

        /// <summary>
        /// Smallest value to convert
        /// </summary>
        private const int _minimumValue = 0;
        /// <summary>
        /// Greatest value to convert 
        /// </summary>
        private const int _maximumValue = 999999999;
    }
}
