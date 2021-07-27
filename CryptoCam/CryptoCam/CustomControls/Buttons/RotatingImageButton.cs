using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CryptoCam.CustomControls.Buttons
{
    class RotatingImageButton : ImageButton
    {
        private CancellationTokenSource cts;
        public RotatingImageButton() : base(){           

            }
        public async Task StartRotating()
        {
           
            
            this.cts = new CancellationTokenSource();
            while (!this.cts.Token.IsCancellationRequested)
            {
                await this.RotateTo(360, 800, Easing.Linear);
                
                await this.RotateTo(0, 0);
                
            }
        }
        public async Task StopRotating()
        {
            this.cts.Cancel();
            
        }
    }
}
