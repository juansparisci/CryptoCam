using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCam.Model
{
    public class FiatCurrency
    {
        private string description;
        private string id;
        public string Description { get => description; set => description = value; }
        public string Id { get => id; set => id = value; }
    }
}
