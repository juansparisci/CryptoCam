using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Android.Util;
using CryptoCam.DependencyServices;
using Xamarin.Forms;

[assembly: Dependency(typeof(CryptoCam.Droid.Services.ImageFromViewService))]
namespace CryptoCam.Droid.Services
{
    public class ImageFromViewService : ICamera
    {
        public async Task<byte[]> GetPreviewFromView()
        {
    
            var bitmap = await CameraPreviewRenderer.GetInstance().GetBitmapPreview();
            byte[] bitmapData;



            using (var stream = new System.IO.MemoryStream())
            {
                bitmap.Compress(Android.Graphics.Bitmap.CompressFormat.Png,100, stream);
                bitmapData = stream.ToArray();
            }


            return bitmapData;
        }

    }
}