using CryptoCamWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tesseract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoCamWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoConverterController : ControllerBase
    {
        private const string trainedDataFolderName = "tessdata";

        // POST api/<CryptoConverter>
        [HttpPost]
        public void Post([FromForm] CryptoCamConvertData request)
        {
            byte[] image;
            using (var ms = new MemoryStream())
            {
                request.Image.CopyTo(ms);
                image = ms.ToArray();
            }
            string amount = getTextFromImage(image, request.DestinationLanguage);

        }

        private string getTextFromImage(byte[] image, string destinationLanguage)
        {

            string result = "";
            string tessPath = Path.Combine(trainedDataFolderName, "");

            using (var engine = new TesseractEngine(tessPath, destinationLanguage, EngineMode.Default))
            {
                using (var img = Pix.LoadFromMemory(image))
                {
                    var page = engine.Process(img);
                    result = page.GetText();
                }
            }
            return String.IsNullOrWhiteSpace(result) ? "Ocr is finished. Return empty" : result;
        }
    }
}
