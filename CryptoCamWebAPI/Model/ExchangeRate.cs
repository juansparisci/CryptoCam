using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Model
{
    public class ExchangeRate
    {
        public FiatCurrency Fiat { get; set; }
        public Dictionary<CryptoCurrency,float> Rates { get; set; }

    }
}
