using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCam.CustomControls.ActivityIndicator
{
    public class LoadingText
    {
        private string text;
        private ILoadingTextPosition ltPosition;

        public string Text { get => text; set => text = value; }
        public ILoadingTextPosition LtPosition { get => ltPosition; set => ltPosition = value; }

        public LoadingText(string text, ILoadingTextPosition position)
        {
            this.text = text;
            ltPosition = position;
        }
    }
}
