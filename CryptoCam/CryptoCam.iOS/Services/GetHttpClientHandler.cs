using CryptoCam.DependencyServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(CryptoCam.iOS.Services.GetHttpClientHandler))]
namespace CryptoCam.iOS.Services
{
   public  class GetHttpClientHandler : IGetHttpClientHandler
    {
        public HttpClientHandler GetInsecureHandler()
        {
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback =( message, cert, chain, errors) =>
                {

                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;

            };
            return handler;
            
        }

        
    }
}