using System;
using System.Threading.Tasks;
using CryptoCam.CustomControls;
using CryptoCam.iOS.Camera;
using CustomRenderer;
using CustomRenderer.iOS;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CameraPreview), typeof(CameraPreviewRenderer))]
namespace CustomRenderer.iOS
{
	public class CameraPreviewRenderer : ViewRenderer<CameraPreview, UICameraPreview>
	{
		private static CameraPreviewRenderer instance;
		UICameraPreview uiCameraPreview;

        public CameraPreviewRenderer()
        {
			instance = this;

		}

        public static CameraPreviewRenderer GetInstance()
		{
			return instance;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
		{
			base.OnElementChanged(e);

			
			if (e.NewElement != null)
			{
				if (Control == null)
				{
					uiCameraPreview = new UICameraPreview(e.NewElement.Camera);
					SetNativeControl(uiCameraPreview);
				}
				
			}
		}

	

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Control.CaptureSession.Dispose();
				Control.Dispose();
			}
			base.Dispose(disposing);
		}

		
		public async Task<NSData> GetUIImagePreview()
		{
		
			var dlgt = new CameraDelegate();
			instance.uiCameraPreview.CapturePhoto(dlgt);
			


			return await dlgt.PhotoTCS.Task;

			
			
		}
	}
}