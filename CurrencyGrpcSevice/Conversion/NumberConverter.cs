using System;

namespace CurrencyGrpcService.Conversion
{
    public interface INumberConverter
    {
        public string ConvertToString(int n, ILocale locale);
    }
    public class NumberConverter : INumberConverter
    {
        public string ConvertToString(int n, ILocale locale)
        {
            // If n is under 100, no prefix is written before the special value
            bool bNIsSmaller100 = (n < 100);
            if (bNIsSmaller100)
            {
                string sForN = locale.GetStringForSpecialValue(n);
                if (sForN != "")
                    return sForN;
            }

            // Find the highest special value smaller or equal to n
            foreach (int nSpecialValue in locale.GetAllSpecialValuesOrderedByDescending())
            {
                if (n >= nSpecialValue)
                {
                    // Compute the value of the string before the special value
                    int nPreSpecialValue = Decimal.ToInt32(Math.Floor(new decimal(n / nSpecialValue)));
                    // If n is smaller than 100, it return an empty string, else it calls recusively ConvertToString
                    string sPreSpecialValue = bNIsSmaller100 ? "" : $"{ConvertToString(nPreSpecialValue, locale)} ";

                    // Compute the value of the string after the special value
                    int nRest = n % nSpecialValue;
                    // If n is 0, it returns an empty string, else it calls recursively ConvertToString
                    string sPostSpecialValue = (nRest == 0) ? "" : ConvertToString(nRest, locale);

                    return $"{sPreSpecialValue}{locale.GetStringForSpecialValue(nSpecialValue)}{locale.GetConnectorAfterSpecialValue(n, nSpecialValue)}{sPostSpecialValue}";
                }
            }

            return "";
        }
    }
}
