using CryptoCam.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCam.WebServices
{
    interface ICryptoConverter_API
    {
        public abstract Task<string> GetTextFromImage(byte[] img);
        public abstract Tuple<List<FiatCurrency>, List<CryptoCurrency>> GetCurrencies();
    }
}
