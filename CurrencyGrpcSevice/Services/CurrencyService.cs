using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CurrencyMessages;
using System;

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
            Conversion.NumberConversion converter = new Conversion.NumberConversion();
            Conversion.NumberEnglish locale = new Conversion.NumberEnglish();
            int nUnits = decimal.ToInt32(Math.Floor(Math.Round(new decimal(request.Value), 2)));
            string sResult = converter.ConvertToString(nUnits, locale);

            return Task.FromResult(new CurrencyString
            {
                Value = sResult
            });
        }
    }
}
