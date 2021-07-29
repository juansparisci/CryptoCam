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
        internal float ovalStartAngle { get; set; }
        internal float secondOvalStartAngle { get; set; }
        internal float ovalSweepAngle { get; set; } 
        internal float innerOvalStartAngle { get; set; }
        internal float innerSecondOvalStartAngle { get; set; }
        internal float innerOvalSweepAngle { get; set; }

        internal SKPaint firstArcPaint { get; set; }
        internal SKPaint secondArcPaint { get; set; }
        private List<LoadingText> dynamicLoadingTexts { get; set; }
        public ArcsBase()
        {
            dynamicLoadingTexts = new List<LoadingText>();
        }

        public virtual void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            this.DrawLoadingText(args);
            

         

        }

        public abstract bool OnTimerClik();
        private void DrawLoadingText(SKPaintSurfaceEventArgs args)
        {

                SKCanvas canvas = args.Surface.Canvas;
            this.dynamicLoadingTexts.ForEach(dlt => {
                if (dlt != null && !String.IsNullOrWhiteSpace(dlt.Text))
                    canvas.DrawText(dlt.Text, 
                                    dlt.LtPosition.GetPosition(args, dlt.TextPaint, dlt.Text), 
                                    dlt.TextPaint);
            });
            
                    


            
        }
        public void AddDynamicLoadingText(LoadingText loadingText)
        {
            this.dynamicLoadingTexts.Add(loadingText);
        }
        public void DeleteDynamicLoadingText(LoadingText loadingText)
        {
            this.dynamicLoadingTexts.Remove(loadingText);
        }
        public void DeleteAllDynamicLoadingTexts()
        {
            this.dynamicLoadingTexts.Clear();
        }
    }
}
