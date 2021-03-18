using CryptoCamWebAPI.Exceptions;
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
    public class OCRController : ControllerBase
    {
        private const string trainedDataFolderName = "tessdata";

        
        [HttpPost]
        public ActionResult Post([FromForm] OCRData request)
        {
           
            try
            {
                byte[] image;
                using (var ms = new MemoryStream())
                {
                    request.Image.CopyTo(ms);
                    image = ms.ToArray();
                }
            //   string amount = this.Compute(Convert.ToDouble(getTextFromImage(image, request.DestinationLanguage))).ToString();
                return Ok(
                    "0.875"
                    );                
                
            }
            catch (OcrException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex);
            }
            
        }


  


        private double Compute(double amount)
        {
            //logic to convert to crypto
            return amount;
        }
        private string getTextFromImage(byte[] image, string destinationLanguage)
        {
            try
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
                if (String.IsNullOrWhiteSpace(result)) {
                    throw new Exception(":( We could not detect any text in the image, please try to take the picture again :)"); }
                else return result;
            }
            catch (Exception ex)
            {
                throw new OcrException(ex.Message,ex);
            }
        }
    }
}
