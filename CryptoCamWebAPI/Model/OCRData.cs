using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Model
{
    public class OCRData
    {
        public IFormFile Image { get; set; }
        public string DestinationLanguage { get; set; }
        public TextFormat TextFormat { get; set; }

    }
}
