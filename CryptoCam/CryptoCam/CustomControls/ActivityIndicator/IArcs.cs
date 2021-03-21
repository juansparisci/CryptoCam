using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace CryptoCam.CustomControls.ActivityIndicator
{
    interface IArcs
    {

        public abstract bool OnTimerClik();
        public abstract void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args);
    }
}
