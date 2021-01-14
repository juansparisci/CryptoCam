using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CryptoCam.DependencyServices
{
    public interface IGetHttpClientHandler
    {
        public HttpClientHandler GetInsecureHandler();
    }
}
