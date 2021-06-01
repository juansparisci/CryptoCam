using CryptoCamWebAPI.Exceptions;
using CryptoCamWebAPI.Model;
using CryptoCamWebAPI.Repositories;
using CryptoCamWebAPI.WebServices;
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

        public CurrenciesController()
        {
        }

        [HttpGet]
        public ActionResult GetCurrencies()
        {

            try
            {              

                return Ok(
                    new
                    {
                        fiats = FiatCurrenciesRepository.GetInstance().GetFiats(),
                        cryptos = CryptoCurrenciesRepository.GetInstance().GetCryptos()
                    });

            }            
            catch (Exception ex)
            {
                return StatusCode(501, ex);
            }

        }


       
      
    }
}
