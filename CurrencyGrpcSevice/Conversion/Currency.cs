using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyGrpcService.Conversion
{
    interface ICurrency
    {
        public string UnitNameSingular();
        public string UnitNamePlural();
        public string CentNameSingular();
        public string CentNamePlural();
    }
}
