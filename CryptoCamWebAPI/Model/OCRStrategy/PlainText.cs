using CryptoCamWebAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tesseract;

namespace CryptoCamWebAPI.Model.OCRStrategy
{
    public class PlainText : OCRStrategy
    {
        public override string DoOCR(byte[] image, string destinationLanguage)
        {
            using (var engine = new TesseractEngine(tessPath, destinationLanguage, EngineMode.Default))
            {
                using (var img = Pix.LoadFromMemory(image))
                {

                    var page = engine.Process(img);
                    string detectedText = page.GetText();
                    if (string.IsNullOrWhiteSpace(detectedText)) throw new OcrException("It was not possible to recognize the text. Please try again.");
                    return detectedText;
                }
            }
        }
    }
}
