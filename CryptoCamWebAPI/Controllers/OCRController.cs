using CryptoCamWebAPI.Exceptions;
using CryptoCamWebAPI.Model;
using CryptoCamWebAPI.Model.OCRStrategy;
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
        private OCRStrategy _OCRStrategy;

        
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
            
                return Ok(
                    getTextFromImage(image, request.DestinationLanguage, request.TextFormat)
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


  


        private string getTextFromImage(byte[] image, string destinationLanguage, TextFormat textFormat)
        {
            try
            {
                //  The selection of OCR Strategy depends on the text format:
                //  if the format is labelprice, an instance of LabelPrice class will do the OCR
                //  if the format is plaintext or other an instance of PlainText class will do the OCR
                //  if there is any new requirment to customize the OCR work in the future, the new strategy must be added as new concrete strategy (inheriting class from OCRStrategy (like LabelPrice and PlainText))
                
                this._OCRStrategy = textFormat == TextFormat.LabelPrice ? new LabelPrice() 
                                                    : (textFormat == TextFormat.PlainText ? new PlainText() 
                                                         : new PlainText());
                
                string  result = this._OCRStrategy.DoOCR(image,destinationLanguage);

                return result;
            }
            catch (Exception ex)
            {
                throw new OcrException(ex.Message,ex);
            }
        }
    }
}
