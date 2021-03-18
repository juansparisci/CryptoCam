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
    public class CryptoConverter_API : ICryptoConverter_API
    {
#if DEBUG
        private static HttpClientHandler insecureHandler = DependencyService.Get<DependencyServices.IGetHttpClientHandler>().GetInsecureHandler();
        private static HttpClient client = new HttpClient(insecureHandler);
#else
                    private static    HttpClient client = new HttpClient();
#endif
        private static string baseAddress = (Device.RuntimePlatform == Device.Android ? "http://10.0.2.2:59865" : "https://localhost:59865")+ "/api";


        public async  Task<string> GetTextFromImage(byte[] img)
        {

                string ocrUri = $"{baseAddress}/OCR/";

                var content = new MultipartFormDataContent();
                var imageContent = new ByteArrayContent(img);
                content.Add(imageContent, "Image", "imgName.imgext");
                content.Add(new StringContent("eng"), "DestinationLanguage");

                var taskPost = await client.PostAsync(ocrUri, content);
                // Task.WaitAll(taskPost);
                if (!taskPost.IsSuccessStatusCode) throw new Exception(await taskPost.Content.ReadAsStringAsync());

                var taskReadString = await taskPost.Content.ReadAsStringAsync();//Result.Content.ReadAsStringAsync();

                // Task.WaitAll(taskReadString);
                var r = taskReadString; //ReadAsString(taskPost).Result;

                return r;
        }


        public  Tuple<List<FiatCurrency>, List<CryptoCurrency>> GetCurrencies()
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
