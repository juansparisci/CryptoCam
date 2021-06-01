using CryptoCamWebAPI.Exceptions;
using CryptoCamWebAPI.Model;
using CryptoCamWebAPI.Model.CoinGecko;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.WebServices.CoinGecko
{
    public class CoinGecko_API : IExchangeRates_API
    {        
        
        public CoinGecko_API()
        {
            IExchangeRates_API.ExchangeRateApiEndpoint = "https://api.coingecko.com/api/v3/exchange_rates";
            IExchangeRates_API.httpClient = new HttpClient();
    }

       public List<ExchangeRate> GetAllRates()
        {
            var ret = new List<ExchangeRate>();
            var rates =  this.GetRatesFromService().Result.Rates;
            IExchangeRates_API.CryptosAccepted.ForEach(crypto =>
            {
                var cryptoInAPI = rates.FirstOrDefault(c=>c.Key.ToUpper()==crypto.Description.ToUpper());
                if (cryptoInAPI.Value != null)
                {
                    ExchangeRate oRate = new ExchangeRate();
                    oRate.Crypto = new CryptoCurrency { Id = cryptoInAPI.Key, Description = cryptoInAPI.Key.ToUpper() };

                    IExchangeRates_API.FiatsAccepted.ForEach(fiat=>
                    {
                        var fiatInApi = rates.FirstOrDefault(f => f.Key.ToUpper() == fiat.Description.ToUpper());
                        if (fiatInApi.Value!=null)
                        {
                            FiatCurrency oFiat = new FiatCurrency { Id=fiatInApi.Key, Description=fiatInApi.Key.ToUpper() };
                            oRate.Rates.Add(oFiat,fiatInApi.Value.Value.Value/cryptoInAPI.Value.Value.Value);
                        }
                    });
                    if (oRate.Rates.Count > 0)
                    {
                        ret.Add(oRate);
                    }
                }
            });


            return ret;
        }


        private async Task<ExchangeRates_CG> GetRatesFromService()
        {
            try
            {
                var response = await IExchangeRates_API.httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, IExchangeRates_API.ExchangeRateApiEndpoint))
              .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();


            
                return JsonConvert.DeserializeObject<ExchangeRates_CG>(responseContent);
            }
            catch (Exception e)
            {
                throw new ExternalWebServiceException(e.Message);
            }

        }
    }

}
