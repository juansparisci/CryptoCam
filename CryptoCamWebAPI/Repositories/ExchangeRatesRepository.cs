
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
        private DateTime lastUpdate;

        public DateTime LastUpdate { get => lastUpdate; }

        public static ExchangeRatesRepository GetInstance()
        {
            if (instance == null) instance = new ExchangeRatesRepository();
            return instance;
        }

        public IList<ExchangeRate> GetRates()
        {
            return this.rates;
        }
        public void AddRate(ExchangeRate rate)
        {
            this.rates.Add(rate);
            lastUpdate = DateTime.Now;
        }       
        public static void DeleteInstance()
        {
            instance = null;
        }
    }
}
