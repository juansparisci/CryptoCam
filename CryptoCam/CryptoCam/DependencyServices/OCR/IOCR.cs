using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCam.DependencyServices.OCR
{
    //This dependency service is used to get the text from the image
    // the first versions will implement OCR_RunningOnServer, that call a webservice to do the OCR process.
    // If it is neccesary (for performance reasons), change to local OCR (Android and iOS) running on the device, the only thing to do
    // is change the implementation class OCR_RunningOnServer for the one that will be created to use in the Android and iOS projects
    public interface IOCR
    {
        public abstract Task<string> GetTextFromImage(byte[] img);

    }
}
