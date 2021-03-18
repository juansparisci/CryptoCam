using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Model
{
    public class ConvertionData
    {
        public double FiatAmount{ get; set; }
        public int FiatID { get; set; }
        public int CryptoID { get; set; }

    }
}
