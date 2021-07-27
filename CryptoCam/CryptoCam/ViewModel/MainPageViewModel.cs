
using CryptoCam.Model;
using CryptoCam.WebServices;
using Helpers.NetStandard;
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
using System.Threading;
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
        private ImageSource focusImgSource;
        private ImageSource capturedImgSource;

        
        public ImageSource convertIconSource { get => ImageSource.FromResource("CryptoCam.Resources.convertButton.png"); }


        public List<FiatCurrency> FiatCurrencies
        {
            get => fiatCurrencies;
            set
            {
                fiatCurrencies = value;
                OnPropertyChanged();
            }

        }
        public List<CryptoCurrency> CryptoCurrencies
        {
            get => cryptoCurrencies;
            set {
                cryptoCurrencies = value;
                OnPropertyChanged();
            }
        }

        public FiatCurrency SelectedFiatCurrency { get => selectedFiatCurrency;
            set {
                selectedFiatCurrency = value;
                OnPropertyChanged();
            } }
        public CryptoCurrency SelectedCryptoCurrency { get => selectedCryptoCurrency;
            set {
                selectedCryptoCurrency = value;
                OnPropertyChanged();
            } }
        public ImageSource FocusImgSource { get => focusImgSource; set { focusImgSource = value; OnPropertyChanged(); } }

    
        public MainPageViewModel()
        {
           
            ScanCommand =  new Command(async () => 
           {
               

               var scanTask = Task.Run(() => this.scan());


               await scanTask;






               await Application.Current.MainPage.Navigation.PushModalAsync(new ResultConvertionPage(((Task<Stream>)scanTask).Result, SelectedFiatCurrency,SelectedCryptoCurrency));
               
          //     ImageFocusBackgroundColor = Color.Transparent;

           }, () => {               
               return true; });

        }
         
        private async Task<Stream> scan()
        {            
            
            var mainPage = ((MainPage)Application.Current.MainPage);

            //get current picture from the camera
            var imgBytes =  await DependencyService.Get<DependencyServices.ICamera>().GetPreviewFromView();

            //get rectangle used to focus the camera
            Xamarin.Forms.Shapes.Rectangle rectFocus = mainPage.RectangleCameraFocus;

            //create variables needed to crop the image:
            //    * rectangle source (screen size)
            //    * destination rectangle (it was got by the rectangle focus)
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0,(int)rectFocus.Y,(int)rectFocus.Width,(int)rectFocus.Height);
            System.Drawing.Rectangle sourceRect = new System.Drawing.Rectangle(0,0,(int)mainPage.Width,(int)mainPage.Height);

            //crop the full size picture from the camera to the focused rectangle
            //using (var imgCrpr = new ImageCropper(imgBytes, sourceRect, destRect, Helpers.NetStandard.ImageCropper.Orientation.Portrait))
            //{
            //     imgCrpr.CroppedImageStream.CopyTo(ret);
            //}
            var imgCrpr = new ImageCropper(imgBytes, sourceRect, destRect, Helpers.NetStandard.ImageCropper.Orientation.Portrait);
           // FocusImgSource = ImageSource.FromStream(()=>imgCrpr.CroppedImageStream);

            return imgCrpr.CroppedImageStream;


            //await Application.Current.MainPage.Navigation.PushAsync(new ResultConversionPage());

            //Make the instance of the cropped image to null to get more free memory 
            // imgCrpr = null;
        }
        private async void loadCurrencies()
        {

            try
            {
                var currencies = await DependencyService.Get<ICryptoConverter_API>()?.GetCurrencies();

                FiatCurrencies = currencies.Fiats;
                CryptoCurrencies = currencies.Cryptos;

                SelectedCryptoCurrency = this.cryptoCurrencies?[0];
                SelectedFiatCurrency = this.fiatCurrencies?[0];
            }
            catch(Exception ex)
            { 
                
                await Application.Current.MainPage.DisplayAlert("Alert", "There was a problem trying to get the currencies from the server. Please, press OK to try again.", "OK");
                loadCurrencies();
            }
        }

        public ICommand ScanCommand { protected set; get; }
       
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void OnAppearing()
        {
            if (CryptoCurrencies == null || FiatCurrencies == null) 
            this.loadCurrencies();
        }
     
       
    }
}
