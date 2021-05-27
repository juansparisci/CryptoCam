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
                    string price = Regex.Match(detectedText, @"[1-9]*\.?[0-9]*[,-.][0-9]*").Value;
                    if (string.IsNullOrWhiteSpace(price)) throw new OcrException("It was not possible to recognize the price. Please try again.");
                    return price;
                }
            }
        }
    }
}
