using System;

namespace CurrencyGrpcService.Conversion
{
    public interface ICurrencyConversion
    {
        public string ConvertToString(double d, ICurrency currency, INumberConversion numberConverter, INumberLocale locale);
    }

    public class CurrencyConversion : ICurrencyConversion
    {
        public string ConvertToString(double dAsDouble, ICurrency currency, INumberConversion numberConverter, INumberLocale locale)
        {
            decimal d = Math.Round(new decimal(dAsDouble), 2);

            int nUnits = decimal.ToInt32(Math.Floor(d));
            string sResultForUnits = numberConverter.ConvertToString(nUnits, locale);
            string sUnitName = (nUnits == 1) ? currency.UnitName.Singular : currency.UnitName.Plural;

            int nCents = decimal.ToInt32((d - nUnits) * 100);
            string sResultForCents = numberConverter.ConvertToString(nCents, locale);
            string sCentName = (nCents == 1) ? currency.CentName.Singular : currency.CentName.Plural;
            string sCents = (nCents > 0) ? $" {sResultForCents} {sCentName}" : "";

            return $"{sResultForUnits} {sUnitName}{sCents}";
        }
    }
}
