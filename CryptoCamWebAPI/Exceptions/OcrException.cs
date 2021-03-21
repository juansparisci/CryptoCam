using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Exceptions
{
    public class OcrException : Exception
    {
        public OcrException(string message) : base(message)
        {
        }

        public OcrException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
