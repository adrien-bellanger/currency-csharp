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
            return Task.FromResult(new CurrencyString
            {
                Value = "Hello " + request.Value
            });
        }
    }
}
