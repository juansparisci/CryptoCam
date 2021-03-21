using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Model
{
    public class ExchangeRate
    {        
        public CryptoCurrency Crypto { get; set; }
        public Dictionary<FiatCurrency, decimal> Rates { get; set; }
        public ExchangeRate()
        {
            Rates = new Dictionary<FiatCurrency, decimal>();
        }
    }
}
