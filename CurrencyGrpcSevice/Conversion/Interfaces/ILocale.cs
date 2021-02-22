using System.Collections.Generic;
using System.Linq;

namespace CurrencyGrpcService.Conversion
{
    /// <summary>
    /// Interface for locales 
    /// </summary>
    public interface ILocale
    {
        /// <summary>
        /// Return all integers with name which is not composed of words from other numbers
        /// </summary>
        /// <returns>Integers with non composed name in descending order</returns>
        public List<int> GetAllSpecialValuesInDescendingOrder();
        
        /// <summary>
        /// Return the word for the given special value 
        /// </summary>
        /// <param name="n">Special value to convert</param>
        /// <returns>The word corresponding to the given integer, if n is not a special value it returns an empty string</returns>
        public string GetWordForSpecialValue(int n);

        /// <summary>
        /// Get the connector to use after the greatest special value in given integer.
        /// </summary>
        /// <param name="n">Integer to convert</param>
        /// <param name="greatestSpecialValueForN">Greatest special value in n</param>
        /// <returns></returns>
        public string GetConnectorAfterSpecialValue(int n, int greatestSpecialValueForN);

        /// <summary>
        /// Get the connector to use between unit and cent
        /// </summary>
        /// <returns>Connector</returns>
        public string GetConnectorBetweenUnitAndCent();
    }
}
