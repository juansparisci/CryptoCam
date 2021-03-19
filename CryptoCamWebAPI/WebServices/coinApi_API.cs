using CryptoCamWebAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.WebServices
{
    public class coinApi_API : IExchangeRates_API
    {   

        List<ExchangeRate> IExchangeRates_API.GetAllRates()
        {
            return new List<ExchangeRate>();
        }
    }
}
