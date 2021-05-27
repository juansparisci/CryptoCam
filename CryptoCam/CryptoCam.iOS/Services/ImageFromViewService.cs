using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoCam.DependencyServices;
using CustomRenderer.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CryptoCam.iOS.Services.ImageFromViewService))]
namespace CryptoCam.iOS.Services
{
    public class ImageFromViewService : ICamera
    {
        public async Task<byte[]> GetPreviewFromView()
        {

            var uiImage =  await CameraPreviewRenderer.GetInstance().GetUIImagePreview();
            if (uiImage != null)
            {
                byte[] bitmapData;

                using (uiImage)
                {
                    bitmapData = new Byte[uiImage.Length];
                    System.Runtime.InteropServices.Marshal.Copy(uiImage.Bytes, bitmapData, 0, Convert.ToInt32(uiImage.Length));
                }

                return bitmapData;
            }else return null;
        }
    }
}