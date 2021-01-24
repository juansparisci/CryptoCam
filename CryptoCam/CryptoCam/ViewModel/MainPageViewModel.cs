using CryptoCam.CustomControls;
using CryptoCam.Model;
using CryptoCam.WebServices;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CryptoCam.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        private List<FiatCurrency> fiatCurrencies;
        private List<CryptoCurrency> cryptoCurrencies;
        private FiatCurrency selectedFiatCurrency;
        private CryptoCurrency selectedCryptoCurrency;
        private string result;

        public List<FiatCurrency> FiatCurrencies
        {
            get => fiatCurrencies;
            set
            {
                fiatCurrencies = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("FiatCurrency"));
                }
            }

        }
        public List<CryptoCurrency> CryptoCurrencies
        {
            get => cryptoCurrencies;
            set {
                cryptoCurrencies = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CryptoCurrencies"));
                }
            }
        }

        public FiatCurrency SelectedFiatCurrency { get => selectedFiatCurrency;
            set {
                selectedFiatCurrency = value;
                //Check and update exchange rate table 
                Result = "You have selected " + value.Description;
            } }
        public CryptoCurrency SelectedCryptoCurrency { get => selectedCryptoCurrency;
            set {
                selectedCryptoCurrency = value;
                // check and update exchange rates table
                Result = "You have selected " + value.Description;
            } }
     //   private ConstraintExpression topFrameHeightConstraintExpression;
       //  public ConstraintExpression TopFrameHeightConstraintExpresion { get { return topFrameHeightConstraintExpression; } set { topFrameHeightConstraintExpression = value; OnPropertyChanged(); } }
        public string Result { get => result; set { result = value; OnPropertyChanged(); } }
       // public ImageSource LogoSource { get => ImageSource.FromResource("CryptoCam.Resources.logo_size_invert.jpg"); }

        public MainPageViewModel()
        {
            this.loadCurrencies();
            ScanCommand = new Command(() =>{ this.scan();},() =>{return true;});
            
        }
         
        private void scan()
        {

            Result = "ScanCommand Pressed";
            var imgBytes = DependencyService.Get<DependencyServices.ICamera>().GetPreviewFromView();
            var mainPage = ((MainPage)Application.Current.MainPage);

            Xamarin.Forms.Shapes.Rectangle rect = mainPage.RectangleCameraFocus;
            SKRect destRect = new SKRect(0, (float)rect.Y, (float)rect.Width, (float)(rect.Y + rect.Height));            
            SKRect sourceRect = new SKRect(0,0,(float)mainPage.Width,(float)mainPage.Height);

            //var y1 = rect.Y;
            //var y2 = y1 + rect.Height;
            //var x1 = 0;
            //var x2 = rect.Width;

            using (var skCanvas = new SKCanvas(SKBitmap.FromImage(SKImage.FromEncodedData(imgBytes))))
            {
                var surface = SKSurface.CreateNull((int)sourceRect.Width, (int)sourceRect.Height);
                surface.Canvas.DrawBitmap(SKBitmap.FromImage(SKImage.FromEncodedData(imgBytes)),sourceRect,destRect);
                using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Png, 80))
                using (var stream = File.OpenWrite(Path.Combine("Resources", "1.png")))
                {
                    // save the data to a stream
                    data.SaveTo(stream);
                }
            }
            


            // var r = WebServices.OCR_API.GetTextFromImage(imgBytes);

        }

        public ICommand ScanCommand { protected set; get; }
       
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void loadCurrencies()
        {
            var currencies = OCR_API.GetCurrencies();
            this.fiatCurrencies = currencies.Item1;
            this.cryptoCurrencies = currencies.Item2;
        }

       
    }
}
