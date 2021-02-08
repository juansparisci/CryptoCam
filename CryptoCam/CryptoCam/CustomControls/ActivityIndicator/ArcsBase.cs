using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace CryptoCam.CustomControls.ActivityIndicator
{
    public abstract class ArcsBase : ContentView, IArcs
    {

        internal SKCanvasView canvas { get; set; }
        internal Stopwatch stopwatch { get; set; }
        internal float OvalStartAngle { get; set; }
        internal float SecondOvalStartAngle { get; set; }
        internal float OvalSweepAngle { get; set; } 
        internal float InnerOvalStartAngle { get; set; }
        internal float InnerSecondOvalStartAngle { get; set; }
        internal float InnerOvalSweepAngle { get; set; }

        internal SKPaint firstArcPaint { get; set; }
        internal SKPaint secondArcPaint { get; set; }

        public abstract void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args);

        public abstract bool OnTimerClik();
    }
}
