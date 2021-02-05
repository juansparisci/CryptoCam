using CryptoCam.DependencyServices;
using CryptoCam.Model;
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
    public class OCR_API
    {
#if DEBUG
        private static HttpClientHandler insecureHandler = DependencyService.Get<DependencyServices.IGetHttpClientHandler>().GetInsecureHandler();
        private static HttpClient client = new HttpClient(insecureHandler);
#else
                    private static    HttpClient client = new HttpClient();
#endif
        private static string baseAddress = Device.RuntimePlatform == Device.Android ? "http://10.0.2.2:59865" : "https://localhost:59865";


        public static string GetConversion(byte[] img)
        {
            string ocrUri = $"{baseAddress}/api/CryptoConverter/";

            var content = new MultipartFormDataContent();
            var imageContent = new ByteArrayContent(img);
            content.Add(imageContent, "Image", "imgName.imgext");
            content.Add(new StringContent("eng"), "DestinationLanguage");

            var taskPost =  client.PostAsync(ocrUri, content);
            Task.WaitAll(taskPost);
            var taskReadString = taskPost.Result.Content.ReadAsStringAsync();
            Task.WaitAll(taskReadString);
            var r = taskReadString.Result; //ReadAsString(taskPost).Result;
            
            return r;
        }
        private static async Task<string> ReadAsString(Task<HttpResponseMessage> taskPost)
        {
            
            return await taskPost.Result.Content.ReadAsStringAsync();
        }
        public static Tuple<List<FiatCurrency>, List<CryptoCurrency>> GetCurrencies()
        {
            //Mock loading the currencies
            var ret = new Tuple<List<FiatCurrency>, List<CryptoCurrency>>(
                        new List<FiatCurrency> { new FiatCurrency { Description = "USD" }, new FiatCurrency { Description = "AUD" }, new FiatCurrency { Description = "EUR" } },
                        new List<CryptoCurrency> { new CryptoCurrency { Description = "BTC" }, new CryptoCurrency { Description = "ETH" }, new CryptoCurrency { Description = "LTC" } }
                        );
            return ret;

        }
    }

}
