using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CryptoCam.DependencyServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(CryptoCam.Droid.Services.GetHttpClientHandler))]
namespace CryptoCam.Droid.Services
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