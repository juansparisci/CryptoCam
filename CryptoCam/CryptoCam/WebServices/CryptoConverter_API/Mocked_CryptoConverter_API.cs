using CryptoCam.DependencyServices;
using CryptoCam.Model;
using CryptoCam.Model.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CryptoCam.WebServices
{
    public class Mocked_CryptoConverter_API : ICryptoConverter_API
    {
#if DEBUG
        private static HttpClientHandler insecureHandler = DependencyService.Get<DependencyServices.IGetHttpClientHandler>().GetInsecureHandler();
        private static HttpClient client = new HttpClient(insecureHandler);
#else
                    private static    HttpClient client = new HttpClient();
#endif
        private static string baseAddress = (Device.RuntimePlatform == Device.Android ? "http://10.0.2.2:59865" : "https://localhost:59865") + "/api";


        public async Task<string> GetTextFromImage(byte[] img)
        {

            return "28000";
        }


        public Task<Currencies> GetCurrencies()
        {
            //Mock loading the currencies
            var ret = new Currencies { Fiats =
                        new List<FiatCurrency> { new FiatCurrency { Description = "USD" }, new FiatCurrency { Description = "AUD" }, new FiatCurrency { Description = "EUR" } },
                        Cryptos = 
                        new List<CryptoCurrency> { new CryptoCurrency { Description = "BTC" }, new CryptoCurrency { Description = "ETH" }, new CryptoCurrency { Description = "LTC" } }
                        };
            return Task<Currencies>.FromResult(ret);

        }

        public async Task<string> Convert(decimal amount, string cryptoId, string fiatID)
        {
            return "0.5236";
        }
    }

}