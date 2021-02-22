namespace CurrencyGrpcService.Conversion
{
    /// <summary>
    /// Implementation of the currency base for dollars
    /// </summary>
    public class Dollar : CurrencyBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Dollar(): base(new CurrencyBase.Word("dollar", "dollars"), new CurrencyBase.Word("cent", "cents"))
        { }
    }
}
