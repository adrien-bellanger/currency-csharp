using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyGrpcService.Conversion
{
    public interface INumberConversion
    {
        public string ConvertToString(int n, INumberLocale locale);
    }
    public class NumberConversion : INumberConversion
    {
        public string ConvertToString(int n, INumberLocale locale)
        {
            if (n == 0)
                return "";

            string sForN = locale.GetStringForSpecialValue(n);
            if (sForN != "")
                return sForN;

            foreach(int nSpecialValue in locale.GetAllSpecialValues())
            {
                if (n > nSpecialValue)
                {
                    int nPreSpecialValue = n >= 100 ? Decimal.ToInt32(Math.Floor(new decimal(n / nSpecialValue))) : 0;
                    string sPreSpecialValue = $"{ConvertToString(nPreSpecialValue, locale)} ";
                    int nRest = n % nSpecialValue;
                    string sSpecialString = (nRest > 0) ? locale.GetSpecialString(n) : " ";
                    return $"{sPreSpecialValue} {locale.GetStringForSpecialValue(nSpecialValue)}{sSpecialString}{ConvertToString(nRest, locale)}";
                }
            }

            return "";
        }
    }
}
