using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCam.CustomControls.ActivityIndicator
{
    public class BottomTextPosition : ILoadingTextPosition
    {
        public SKPoint GetPosition(SKPaintSurfaceEventArgs args, SKPaint skTextPaint, string text)
        {
            SKCanvas canvas = args.Surface.Canvas;
            SKRect textBounds = new SKRect();
            skTextPaint.MeasureText(text, ref textBounds);
            return new SKPoint { X = (args.Info.Width / 2 - textBounds.MidX), Y = (args.Info.Height-(textBounds.Height * 2)) };
        }
    }
}
