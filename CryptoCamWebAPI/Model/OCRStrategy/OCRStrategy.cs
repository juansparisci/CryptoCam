using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Model.OCRStrategy
{
    public abstract class OCRStrategy
    {
        internal string tessPath = Path.Combine("tessdata", "");
        public OCRStrategy()
        {
        }
        public abstract string DoOCR(byte[] image, string destinationLanguage);
    }
}
