using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Model
{
    public class ConvertionData
    {
        public decimal FiatAmount{ get; set; }
        public string FiatID { get; set; }
        public string CryptoID { get; set; }

    }
}
