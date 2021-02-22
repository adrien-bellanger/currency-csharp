using System;

namespace CurrencyGrpcService.Conversion
{
    public interface ICurrencyConverter
    {
        public string ConvertToWords(double d, ICurrency currency, ILocale locale);
    }

    public class CurrencyConverter : ICurrencyConverter
    {
        public CurrencyConverter(INumberConverter numberConverter)
        {
            NumberConverter = numberConverter;
        }
        
        public string ConvertToWords(double dAsDouble, ICurrency currency, ILocale locale)
        {
            decimal d = Math.Round(new decimal(dAsDouble), 2);

            int nUnits = decimal.ToInt32(Math.Floor(d));
            string sResultForUnits = NumberConverter.ConvertToString(nUnits, locale);
            string sUnitName = (nUnits == 1) ? currency.UnitName.Singular : currency.UnitName.Plural;

            int nCents = decimal.ToInt32((d - nUnits) * 100);
            string sResultForCents = NumberConverter.ConvertToString(nCents, locale);
            string sCentName = (nCents == 1) ? currency.CentName.Singular : currency.CentName.Plural;
            string sCents = (nCents > 0) ? $" {locale.GetConnectorBetweenUnitAndCent()} {sResultForCents} {sCentName}" : "";

            return $"{sResultForUnits} {sUnitName}{sCents}";
        }

        private INumberConverter NumberConverter { get; set; }
    }
}
