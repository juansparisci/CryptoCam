
using CryptoCamWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Repositories
{
    public class CryptoCurrenciesRepository
    {
        private static CryptoCurrenciesRepository instance;
        private IList<CryptoCurrency> cryptoCurrencies;
        private CryptoCurrenciesRepository() {
            this.cryptoCurrencies = new List<CryptoCurrency>();
        }
        public static CryptoCurrenciesRepository GetInstance()
        {
            if (instance == null) instance = new CryptoCurrenciesRepository();
            return instance;
        }


        public void AddCrypto(CryptoCurrency cryptoCurrency)
        {
            this.cryptoCurrencies.Add(cryptoCurrency);
        }
        public IEnumerable<CryptoCurrency> GetCryptos()
        {
            return this.cryptoCurrencies;
        }
        public CryptoCurrency GetCryptoByID(string id)
        {
            return this.cryptoCurrencies.FirstOrDefault(c=>c.Id==id);
        }
        public static void DeleteInstance()
        {
            instance = null;
        }
    }
}
