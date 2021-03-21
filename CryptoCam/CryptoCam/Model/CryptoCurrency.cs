using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCam.Model
{
    public class CryptoCurrency
    {

        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
