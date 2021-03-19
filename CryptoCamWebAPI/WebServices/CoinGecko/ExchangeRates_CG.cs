using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.WebServices.CoinGecko
{
    public class ExchangeRates_CG
    {
        [JsonProperty("rates")]
        public Dictionary<string, Rate_CG> Rates { get; set; }
    }
    public class Rate_CG
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("value")]
        public decimal? Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
