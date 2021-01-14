using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Model
{
    public class CryptoCamConvertData
    {
        public IFormFile Image { get; set; }
        public string DestinationLanguage { get; set; }
        public string FiatID { get; set; }
        public string CryptoID { get; set; }
    }
}
