using CryptoCamWebAPI.Exceptions;
using CryptoCamWebAPI.Model;
using CryptoCamWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoConverterController : Controller
    {
        [HttpGet]
        public IActionResult Get([FromQuery] ConvertionData request)
        {
            try
            {
                var rateByCrypto = ExchangeRatesRepository.GetInstance().GetRates().FirstOrDefault(r => r.Crypto.Id == request.CryptoID);
                if (rateByCrypto == null) throw new ConverterException("The crypto currency "+request.CryptoID+" was not found.");

                var rate = rateByCrypto.Rates.FirstOrDefault(rc => rc.Key.Id == request.FiatID);
                if(rate.Key==null) throw new ConverterException("The rate "+request.FiatID+" to "+request.CryptoID+" was not found.");

                return Ok(request.FiatAmount / rate.Value);
            }
            catch (ExternalWebServiceException ex)
            {
                return StatusCode(550, ex);
            }
            catch(ConverterException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex); ;
            }

        }
    }
}
