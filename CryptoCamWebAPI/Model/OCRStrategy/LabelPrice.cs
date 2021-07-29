using CryptoCamWebAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tesseract;

namespace CryptoCamWebAPI.Model.OCRStrategy
{
    public class LabelPrice : OCRStrategy
    {
        public override string DoOCR(byte[] image, string destinationLanguage)
        {
            using (var engine = new TesseractEngine(tessPath, destinationLanguage, EngineMode.Default))
            {
                using (var img = Pix.LoadFromMemory(image))
                {
                    var page = engine.Process(img);                    
                    string detectedText = page.GetText();                    
                    string price = this.getPriceFromText(detectedText);

                    decimal dPrice =0;

                    if (string.IsNullOrWhiteSpace(price) || (!decimal.TryParse(price,out dPrice)) || dPrice==0 ) 
                        throw new OcrException("It was not possible to recognize the price. Please try again.");
                    
                    return price;
                }
            }
        }
        private string getPriceFromText(string text)
        {
            // First attempts to get the price with decimal
            
            string price = Regex.Match(text, @"[1-9]*\.?[0-9]*[,-.][0-9]*").Value;
            
            
            // If it is not possible to get the price with decimals attempts to get the price without decimals.
            
            if (string.IsNullOrWhiteSpace(price)) price = Regex.Match(text, @"[0-9]*\.?[0-9]").Value;

            decimal dPrice = 0;

            // If it is not possible to identify a price in the text, throw an exception
            if (string.IsNullOrWhiteSpace(price) || (!decimal.TryParse(price, out dPrice)) || dPrice == 0)
                throw new OcrException("It was not possible to recognize the amount in the image. Please try again.");

            
            return price;
        }
    }
}
