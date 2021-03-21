using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCam.Model.Responses
{
    public class Currencies
    {
        [JsonProperty("fiats")]
        public List<FiatCurrency> Fiats { get; set; }

        [JsonProperty("cryptos")]
        public List<CryptoCurrency> Cryptos{ get; set; }

    }
}
