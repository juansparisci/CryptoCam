
using CryptoCamWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Repositories
{
    public class FiatCurrenciesRepository
    {
        private static FiatCurrenciesRepository instance;
        private IList<FiatCurrency> fiatCurrencies;
        private FiatCurrenciesRepository() {
            this.fiatCurrencies = new List<FiatCurrency>();
        }
        public static FiatCurrenciesRepository GetInstance()
        {
            if (instance == null) instance = new FiatCurrenciesRepository();
            return instance;
        }


        public void AddFiat(FiatCurrency fiatCurrency)
        {
            this.fiatCurrencies.Add(fiatCurrency);
        }
        public IEnumerable<FiatCurrency> GetFiats()
        {
            return this.fiatCurrencies;
        }
        public FiatCurrency GetFiatByID(string id)
        {
            return this.fiatCurrencies.FirstOrDefault(f=>f.Id==id);
        }
        public static void DeleteInstance()
        {
            instance = null;
        }
    }
}
