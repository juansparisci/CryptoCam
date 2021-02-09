using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCam.CustomControls.ActivityIndicator
{
    public  class LoadingText
    {
        private string text;
        private ILoadingTextPosition ltPosition;
        SKPaint textPaint;

        public string Text { get => text; set => text = value; }
        public ILoadingTextPosition LtPosition { get => ltPosition; set => ltPosition = value; }
        public SKPaint TextPaint { get => textPaint; set => textPaint = value; }

        public LoadingText(string text, ILoadingTextPosition position)
        {
            //Set text style
            this.textPaint = new SKPaint { Color = SKColors.Black, TextSize = 48 };

            //Set text
            this.text = text;

            //Set position
            this.ltPosition = position;

        }
    }
}
