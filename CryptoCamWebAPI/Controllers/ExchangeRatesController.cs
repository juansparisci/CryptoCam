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
    public class ExchangeRatesController : ControllerBase
    {

        public ExchangeRatesController()
        {
        }

        [HttpGet]
        public ActionResult GetRates()
        {

            try
            {

                var ret= Ok(
                    new
                    {
                        rates = ExchangeRatesRepository.GetInstance().GetRates(),
                        lastUpdate = ExchangeRatesRepository.GetInstance().LastUpdate
                    });
                return ret;

            }            
            catch (Exception ex)
            {
                return StatusCode(501, ex);
            }

        }


       
      
    }
}
