using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CryptoCam.DependencyServices;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CryptoCam.iOS.Services.ImageFromViewService))]
namespace CryptoCam.iOS.Services
{
    public class ImageFromViewService : ICamera
    {
        public byte[] GetPreviewFromView()
        {
            throw new NotImplementedException();
        }
/*
        public byte[] ImageToByteArray(string path)
        {
            throw new NotImplementedException();
        }
        */
        public string RecognizeText(byte[] imageBytes)
        {
            throw new NotImplementedException();
        }
    }
}