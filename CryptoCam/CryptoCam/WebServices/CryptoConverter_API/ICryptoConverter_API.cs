using CryptoCam.Model;
using CryptoCam.Model.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCam.WebServices
{
    interface ICryptoConverter_API
    {
        public abstract Task<string> GetTextFromImage(byte[] img);
        public abstract Task<Currencies> GetCurrencies();
        public abstract Task<string> Convert(decimal amount, string cryptoId, string fiatID);
    }
}
