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
    public class Mock_CryptoConverter_API : ICryptoConverter_API
    {
        private bool returnError;


        public Mock_CryptoConverter_API()
        {
            returnError = false;
        }
        
        
        public async Task<string> GetTextFromImage(byte[] img)
        {
            await Task.Delay(1000);
            if (returnError) throw new Exception("It was not possible to identify the amount, please try scanning again.");
            return "28000";
        }


        public async Task<Currencies> GetCurrencies()
        {
            await Task.Delay(1000);
            if (returnError) throw new Exception("There was an error trying to get the currencies values from the server. :( Please try again later.");

            var ret = new Currencies { Fiats =
                        new List<FiatCurrency> { new FiatCurrency { Id = "usd", Description = "USD" }, new FiatCurrency { Id = "aud", Description = "AUD" }, new FiatCurrency { Id = "eur", Description = "EUR" } },
                        Cryptos = 
                        new List<CryptoCurrency> { new CryptoCurrency { Id="btc", Description = "BTC" }, new CryptoCurrency { Id="eth", Description = "ETH" }, new CryptoCurrency { Id="ltc", Description = "LTC" } }
                        };
            return ret;

        }

        public async Task<string> Convert(decimal amount, string cryptoId, string fiatID)
        {
            await Task.Delay(1000);
            if (returnError) throw new Exception("There was an error trying to convert the selected values.");
            return "0.5236";
        }
    }

}