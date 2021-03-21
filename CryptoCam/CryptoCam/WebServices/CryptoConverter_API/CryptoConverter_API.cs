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


        public async Task<Currencies> GetCurrencies()
        {
            var ret = new Currencies();

            string ocrUri = $"{baseAddress}/Currencies/";
            var taskGet = await client.GetAsync(ocrUri);

            
            if (!taskGet.IsSuccessStatusCode) throw new Exception(await taskGet.Content.ReadAsStringAsync());

            var taskReadString = await taskGet.Content.ReadAsStringAsync();
            ret = JsonConvert.DeserializeObject<Currencies>(taskReadString);
             
            return ret;

        }

        public async Task<string> Convert(decimal amount, string cryptoId, string fiatID)
        {
            var ret = "";
            string parameters = $"?FiatAmount={amount}&FiatID={fiatID}&CryptoID={cryptoId}";
            string ocrUri = $"{baseAddress}/CryptoConverter/{parameters}";
            

            var taskGet = await client.GetAsync(ocrUri);

            if (!taskGet.IsSuccessStatusCode) throw new Exception(await taskGet.Content.ReadAsStringAsync());

            var taskReadString = await taskGet.Content.ReadAsStringAsync();
            ret = taskReadString;

            return ret;
        }
    }

}
