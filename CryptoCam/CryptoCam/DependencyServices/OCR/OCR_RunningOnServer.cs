using CryptoCam.WebServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CryptoCam.DependencyServices.OCR
{
    class OCR_RunningOnServer : IOCR
    {
        public Task<string> GetTextFromImage(byte[] img)
        {
            return DependencyService.Get<ICryptoConverter_API>()?.GetTextFromImage(img);
        }
    }
}
