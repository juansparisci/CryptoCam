using AVFoundation;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace CryptoCam.iOS.Camera
{
	public class CameraDelegate : AVCapturePhotoCaptureDelegate
	{
		public TaskCompletionSource<NSData> PhotoTCS;
		public NSData PhotoData;
		public CameraDelegate()
		{
			PhotoTCS = new TaskCompletionSource<NSData>(); 
		}
		
        public override void WillBeginCapture(AVCapturePhotoOutput captureOutput, AVCaptureResolvedPhotoSettings resolvedSettings)
        {
		
        }

        public override void DidFinishCapture(AVCapturePhotoOutput captureOutput, AVCaptureResolvedPhotoSettings resolvedSettings, NSError error)
		{
			
		}

		public override void DidFinishProcessingPhoto(AVCapturePhotoOutput output, AVCapturePhoto photo, NSError error)
		{
		

				if (error == null)
				{

				
				PhotoTCS.SetResult(photo.FileDataRepresentation);
				}
				else { throw new Exception("Algo pasó al tratar de tomar la foto"); }

		}
	}
}