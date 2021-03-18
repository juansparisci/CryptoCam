using CryptoCamWebAPI.Exceptions;
using CryptoCamWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tesseract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoCamWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        


        [HttpGet]
        public ActionResult GetCurrencies()
        {

            try
            {
                var ret = new Tuple<List<FiatCurrency>, List<CryptoCurrency>>(
                          new List<FiatCurrency> { new FiatCurrency { Description = "USD" }, new FiatCurrency { Description = "AUD" }, new FiatCurrency { Description = "EUR" } },
                          new List<CryptoCurrency> { new CryptoCurrency { Description = "BTC" }, new CryptoCurrency { Description = "ETH" }, new CryptoCurrency { Description = "LTC" } }
                          );
                return Ok(ret);

            }
            catch (Exception ex)
            {
                return StatusCode(501, ex);
            }

        }


       
      
    }
}
