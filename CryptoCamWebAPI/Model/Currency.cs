using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Model
{
    public abstract class Currency
    {

        public string Description { get; set; }
        public string Id { get; set; }

        public override string ToString()
        {
            return this.Id;
        }
    }
}
