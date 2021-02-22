namespace CurrencyGrpcService.Conversion
{
    /// <summary>
    /// Base implementation for currencies
    /// </summary>
    public class CurrencyBase : ICurrency
    {
        /// <summary>
        /// Base implementation for words. It defines a singular and its plural.
        /// </summary>
        protected class Word : ICurrency.IWord
        {
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="sSingular">Word's singular</param>
            /// <param name="sPlural">Word's plural</param>
            public Word(string sSingular, string sPlural)
            {
                Singular = sSingular;
                Plural = sPlural;
            }

            public string Singular { get; private set; }
            public string Plural { get; private set; }
        }

        /// <summary>
        /// Constructor for a currency
        /// </summary>
        /// <param name="unitName">Unit's names</param>
        /// <param name="centName">Cent's names</param>
        protected CurrencyBase(ICurrency.IWord unitName, ICurrency.IWord centName)
        {
            UnitName = unitName;
            CentName = centName;
        }

        public ICurrency.IWord UnitName { get; private set; }
        public ICurrency.IWord CentName { get; private set; }
    }
}
