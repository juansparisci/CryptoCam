using CryptoCamWebAPI.Exceptions;
using CryptoCamWebAPI.Repositories;
using CryptoCamWebAPI.WebServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncAssetsRepositoriesController : Controller
    {
        private IExchangeRates_API exchangeRates_API;
        public SyncAssetsRepositoriesController(IExchangeRates_API exchangeRates_API) 
        {
            this.exchangeRates_API = exchangeRates_API;
        }
     

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                UpdateRepositories();
                return Ok();
            }
            catch (Exception ex){
                return StatusCode(501, ex);
            }
        }



        internal async Task UpdateRepositories()
        {
            
                this.DeleteRepositoriesInstances();
                var ratesRepository = ExchangeRatesRepository.GetInstance();
                var fiatRepository = FiatCurrenciesRepository.GetInstance();
                var cryptoRepository = CryptoCurrenciesRepository.GetInstance();

                (await exchangeRates_API?.GetAllRates()).ForEach(ratesByCrypto =>
                {

                    var cryptoInRepo = cryptoRepository.GetCryptoByID(ratesByCrypto.Crypto.Id);
                    if (cryptoInRepo == null) cryptoRepository.AddCrypto(ratesByCrypto.Crypto);

                    foreach (var k in ratesByCrypto.Rates.Keys)
                    {
                        var fiatInRepo = fiatRepository.GetFiatByID(k.Id);
                        if (fiatInRepo == null) fiatRepository.AddFiat(k);
                    }

                    ratesRepository.AddRate(ratesByCrypto);
                });
            
        }


        private void DeleteRepositoriesInstances()
        {
            ExchangeRatesRepository.DeleteInstance();

            CryptoCurrenciesRepository.DeleteInstance();

            FiatCurrenciesRepository.DeleteInstance();
        }

    }
}
