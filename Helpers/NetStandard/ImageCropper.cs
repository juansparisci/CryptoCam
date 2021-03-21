using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace Helpers.NetStandard
{
    public class ImageCropper : IDisposable
    {
        private SKBitmap _ImgBitmap;
        private SKRectI _DestRect;
        private SKImage _CroppedImage;
        private Stream _CroppedImageStream;
        private Orientation _Orientation;

        public SKImage CroppedImage { get => _CroppedImage;}
        public Stream CroppedImageStream { get => _CroppedImageStream; }
        public ImageCropper(byte[] imgByte, Rectangle sourceRect, Rectangle destRect, Orientation orientation)
        {
            // set values
            
            this._Orientation = orientation;
            this._DestRect = new SKRectI(destRect.Left, destRect.Top, destRect.Right, destRect.Bottom);

            //get SKImage from byte array
            this._ImgBitmap = SKBitmap.Decode(imgByte);
            var image = SKImage.FromBitmap(_ImgBitmap);
            
            
            //Compute the proportion of the cropped rectangule against the original picture size
            this.MakeProportion(image,new SKRectI(sourceRect.Left,sourceRect.Top,sourceRect.Right,sourceRect.Bottom));



            //crop the image
            this._CroppedImage = image.Subset(this._DestRect);

            //// encode the image
            var encodedData = this._CroppedImage.Encode(SKEncodedImageFormat.Png, 100);

            //// get a stream that can be saved to disk/memory/etc
            this._CroppedImageStream = encodedData.AsStream();
        }

        private void MakeProportion(SKImage image, SKRectI sourceRect)
        {
            float proportion =  (this._Orientation == Orientation.Portrait) 
                ? ((float)image.Height) / sourceRect.Height 
                : ((float)image.Width) / sourceRect.Width;
            this._DestRect.Top = (int)(this._DestRect.Top * proportion);
            this._DestRect.Bottom = (int)(this._DestRect.Bottom * proportion);
            this._DestRect.Left = (int)(this._DestRect.Left * proportion);
            this._DestRect.Right = (int)(this._DestRect.Right * proportion);
        }

        public void Dispose()
        {           
        }

        public enum Orientation
        {
            Portrait,
            Landscape
        }
    }
    
}
