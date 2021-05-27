using System;
using System.Linq;
using System.Threading.Tasks;
using AVFoundation;
using CoreGraphics;
using CryptoCam.CustomControls;
using Foundation;
using UIKit;

namespace CryptoCam.iOS.Camera
{
	public class UICameraPreview : UIView 
	{
		AVCapturePhotoOutput photoOutput;
		AVCaptureVideoPreviewLayer previewLayer;
		CameraOptions cameraOptions;


		public AVCaptureSession CaptureSession { get; private set; }

		public bool IsPreviewing { get; set; }

		public UICameraPreview(CameraOptions options)
		{
			cameraOptions = options;
			IsPreviewing = false;
			Initialize();
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			if (previewLayer != null)
				previewLayer.Frame = Bounds;
		}


		

		void Initialize()
		{
			CaptureSession = new AVCaptureSession();
			previewLayer = new AVCaptureVideoPreviewLayer(CaptureSession)
			{
				Frame = Bounds,
				VideoGravity = AVLayerVideoGravity.ResizeAspectFill,
				 Orientation= AVCaptureVideoOrientation.Portrait
			};
			
			

			var videoDevices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);
			var cameraPosition = (cameraOptions == CameraOptions.Front) ? AVCaptureDevicePosition.Front : AVCaptureDevicePosition.Back;
			var device = videoDevices.FirstOrDefault(d => d.Position == cameraPosition);

			if (device == null)
			{
				return;
			}

			NSError error;
			var input = new AVCaptureDeviceInput(device, out error);
			CaptureSession.AddInput(input);
			Layer.AddSublayer(previewLayer);


			photoOutput = new AVCapturePhotoOutput();

			CaptureSession.AddOutput(photoOutput);


			CaptureSession.StartRunning();
			IsPreviewing = true;
		}

		public void CapturePhoto(CameraDelegate dlgt)
		{
			var settings = AVCapturePhotoSettings.Create();
			settings.IsAutoStillImageStabilizationEnabled = true;
			settings.FlashMode = AVCaptureFlashMode.Off;
			
			
			photoOutput.CapturePhoto(settings, dlgt);


			
		}

    }

	
}