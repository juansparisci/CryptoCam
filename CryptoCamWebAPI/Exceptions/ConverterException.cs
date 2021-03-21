using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Exceptions
{
    public class ConverterException : Exception
    {
        public ConverterException(string message) : base(message)
        {
        }

        public ConverterException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
