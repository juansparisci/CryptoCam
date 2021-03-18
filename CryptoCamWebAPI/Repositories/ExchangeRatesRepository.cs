
using CryptoCamWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Repositories
{
    public class ExchangeRatesRepository
    {
        private static ExchangeRatesRepository instance;
        private IList<ExchangeRate> rates;
        private ExchangeRatesRepository() {
            rates = new List<ExchangeRate>();
        }
        public static ExchangeRatesRepository GetInstance()
        {
            if (instance == null) instance = new ExchangeRatesRepository();
            return instance;
        }


        public void AddRate(ExchangeRate rate)
        {
            this.rates.Add(rate);
        }
        public IEnumerable<FiatCurrency> GetFiats()
        {
            return rates.Select(r=>r.Fiat);
        }
        public IEnumerable<CryptoCurrency> GetCryptos()
        {
            return rates.Select(r => r.Rates.Keys).SelectMany(s => s).GroupBy(g => g.Id).Select(g => g.First());
        }
    }
}
