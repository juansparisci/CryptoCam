using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI.Exceptions
{
    public class ExternalWebServiceException : Exception
    {
        public ExternalWebServiceException(string message) : base(message)
        {
        }

        public ExternalWebServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
