using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CurrencyMessages;

namespace CurrencyGrpcService
{
    public class CurrencyService : CurrencyMessages.Currency.CurrencyBase
    {
        private readonly ILogger<CurrencyService> _logger;
        public CurrencyService(ILogger<CurrencyService> logger)
        {
            _logger = logger;
        }

        public override Task<CurrencyString> ConvertDollarToString(CurrencyNumber request, ServerCallContext context)
        {
            Conversion.INumberConverter numberConverter = new Conversion.NumberConverter();
            Conversion.ICurrencyConverter currenccyConverter = new Conversion.CurrencyConverter(numberConverter);
            Conversion.ICurrency currency = new Conversion.Dollar();
            Conversion.NumberEnglish locale = new Conversion.NumberEnglish();
            string sResult = currenccyConverter.ConvertToWords(request.Value, currency, locale);

            return Task.FromResult(new CurrencyString
            {
                Value = sResult
            });
        }
    }
}
