namespace CurrencyGrpcService.Conversion
{
    /// <summary>
    /// Interface for currency, for this project it contains only the names for unit and cent.
    /// </summary>
    public interface ICurrency
    {
        /// <summary>
        /// Interface for words. It defines a singular and its plural.
        /// </summary>
        public interface IWord
        {
            /// <summary>
            /// Singular of this word
            /// </summary>
            public string Singular { get; }
            /// <summary>
            /// Plural of this word
            /// </summary>
            public string Plural { get; }
        }

        /// <summary>
        /// Unit's name for this currency (e.g. dollar)
        /// </summary>
        public IWord UnitName { get; }
        /// <summary>
        /// Cent's name for this currency (e.g. cent)
        /// </summary>
        public IWord CentName { get; }
    }
}
