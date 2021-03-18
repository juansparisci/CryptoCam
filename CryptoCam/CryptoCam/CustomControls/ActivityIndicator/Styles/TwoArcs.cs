using CryptoCam.CustomControls.ActivityIndicator;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace CryptoCam.CustomControls.ActivityIndicator
{
    public partial class TwoArcs : ArcsBase
    {
        public TwoArcs(LoadingText loadingText=null):base(loadingText)
        { 
            OvalStartAngle = 90; //outer arc start angle
            SecondOvalStartAngle = 270; //outer arc start angle
            OvalSweepAngle = 50; //outer arcg sweep angle from the start angle position   
            /// outer arc paint style
            /// defined the style as stroke
            /// color of the stroke using hsl format * you can change the color code here*
            /// width of the outer arc
            firstArcPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,

                Color = SKColor.FromHsl(240, 8, 66),
                StrokeWidth = 25,
                IsAntialias = true

            };
            /// middle arc paint style
            /// defined the style as stroke
            /// color of the stroke using hsl format * you can change the color code here*
            /// width of the middle arc
            secondArcPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,

                Color = SKColor.FromHsl(341, 100, 45),
                StrokeWidth = 25,
                IsAntialias = true,
                StrokeCap = SKStrokeCap.Round

            };





            canvas = new SKCanvasView();
            canvas.PaintSurface += OnCanvasViewPaintSurface;
            Content = canvas;
            Device.StartTimer(TimeSpan.FromMilliseconds(16), OnTimerClik);
        }

        public override bool OnTimerClik()
        {
            OvalStartAngle += 5;
            SecondOvalStartAngle += 5;
            canvas.InvalidateSurface();
            return true;
        }

        
        public override void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            /*
             * 500 is the diameter of the outer arc
             * each inner arc from the outer arc will get reduced by 50
             *you can change the value according to make the arc smaller or bigger
             * */

            float left, right;
            float top, bottom;
            right = left = (info.Width - 500) / 2; //get the left and right postions to support all the devices
            top = bottom = (info.Height - 500) / 2;//get the top and bottom postions to support all the devices

            //first Arc
            SKRect rect = new SKRect(left, top, info.Width - right, info.Height - bottom);

            canvas.DrawCircle(info.Width / 2, info.Height / 2, 250, firstArcPaint);
            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, OvalStartAngle, OvalSweepAngle);
                canvas.DrawPath(path, secondArcPaint);
            }
            using (SKPath path = new SKPath())
            {
                path.AddArc(rect, SecondOvalStartAngle, OvalSweepAngle);
                canvas.DrawPath(path, secondArcPaint);
            }
            base.OnCanvasViewPaintSurface(sender,args);
        }
    }
}