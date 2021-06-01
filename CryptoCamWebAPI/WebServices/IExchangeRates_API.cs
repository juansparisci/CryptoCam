using CryptoCamWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.WebServices
{
    public interface IExchangeRates_API
    {
        internal static string ExchangeRateApiEndpoint;
        internal static  HttpClient httpClient;

        public static readonly List<FiatCurrency> FiatsAccepted = new List<FiatCurrency> { new FiatCurrency {Description = "ARS" }, new FiatCurrency {Description = "USD" }, new FiatCurrency {Description = "AUD" }, new FiatCurrency {Description = "EUR" } };
        public static readonly List<CryptoCurrency> CryptosAccepted = new List<CryptoCurrency> { new CryptoCurrency {Description = "BTC" }, new CryptoCurrency {Description = "ETH" }, new CryptoCurrency {Description = "SATS" }, new CryptoCurrency {Description = "LTC" } };


        public abstract List<ExchangeRate> GetAllRates();
    }
}
