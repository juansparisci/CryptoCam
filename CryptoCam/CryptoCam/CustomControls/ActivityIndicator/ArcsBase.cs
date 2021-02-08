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
        internal LoadingText loadingText { get; set; }
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
        public ArcsBase(LoadingText loadingText = null)
        {
            this.loadingText = loadingText;
        }

        public virtual void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            if(this.loadingText!=null&&!String.IsNullOrWhiteSpace(this.loadingText.Text))this.DrawLoadingText(args);
        }

        public abstract bool OnTimerClik();
        private void DrawLoadingText(SKPaintSurfaceEventArgs args)
        {

                SKCanvas canvas = args.Surface.Canvas;
                SKPaint skTextPaint = new SKPaint { Color = SKColors.Black, TextSize = 48};              

                canvas.DrawText(this.loadingText.Text,this.loadingText.LtPosition.GetPosition(args,skTextPaint,loadingText.Text) , skTextPaint);
            
        }
    }
}
