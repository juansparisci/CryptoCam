using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCam.CustomControls.ActivityIndicator
{
    public interface ILoadingTextPosition
    {
        public SKPoint GetPosition(SKPaintSurfaceEventArgs args, SKPaint skTextPaint, string text);
    }
}
