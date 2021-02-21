namespace CurrencyGrpcService.Conversion
{
    public interface ICurrency
    {
        public interface IName
        {
            public string Singular { get; }
            public string Plural { get; }
        }
        public IName UnitName { get; }
        public IName CentName { get; }
    }
    public class CurrencyBase : ICurrency
    {
        protected class Name : ICurrency.IName
        {
            public Name(string sSingular, string sPlural)
            {
                Singular = sSingular;
                Plural = sPlural;
            }
            public string Singular { get; private set; }
            public string Plural { get; private set; }
        }

        protected CurrencyBase(ICurrency.IName unitName, ICurrency.IName centName)
        {
            UnitName = unitName;
            CentName = centName;
        }

        public ICurrency.IName UnitName { get; private set; }
        public ICurrency.IName CentName { get; private set; }
    }

    public class Dollar : CurrencyBase
    {
        public Dollar(): base(new CurrencyBase.Name("dollar", "dollars"), new CurrencyBase.Name("cent", "cents"))
        { }
    }
}
