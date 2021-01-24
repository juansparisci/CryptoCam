using CryptoCam.CustomControls;
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
            } }
        public CryptoCurrency SelectedCryptoCurrency { get => selectedCryptoCurrency;
            set {
                selectedCryptoCurrency = value;
                // check and update exchange rates table
            } }
        public ImageSource FocusImgSource { get => focusImgSource; set { focusImgSource = value; OnPropertyChanged(); } }

    
        public MainPageViewModel()
        {
            this.loadCurrencies();
            ScanCommand = new Command(async () => 
            { 
                await Application.Current.MainPage.Navigation.PushModalAsync(new ResultConversionPage(this.scan(),SelectedFiatCurrency,SelectedCryptoCurrency)); 
            }, () => { return true; });


            /*new Command(async () => {                
            await Application.Current.MainPage.Navigation.PushModalAsync(new ResultConversionPage());
        });*/



        }
         
        private Stream scan()
        {
            
            var mainPage = ((MainPage)Application.Current.MainPage);

            //get current picture from the camera
            var imgBytes = DependencyService.Get<DependencyServices.ICamera>().GetPreviewFromView();

            //get rectangle used to focus the camera
            Xamarin.Forms.Shapes.Rectangle rectFocus = mainPage.RectangleCameraFocus;

            //create variables needed to crop the image:
            //    * rectangle source (screen size)
            //    * destination rectangle (it was got by the rectangle focus)
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0,(int)rectFocus.Y,(int)rectFocus.Width,(int)rectFocus.Height);
            System.Drawing.Rectangle sourceRect = new System.Drawing.Rectangle(0,0,(int)mainPage.Width,(int)mainPage.Height);

            //crop the full size picture from the camera to the focused rectangle
            using (var imgCrpr = new ImageCropper(imgBytes, sourceRect, destRect, Helpers.NetStandard.ImageCropper.Orientation.Portrait))
            {
                return imgCrpr.CroppedImageStream;
            }



            //await Application.Current.MainPage.Navigation.PushAsync(new ResultConversionPage());

            //Make the instance of the cropped image to null to get more free memory 
           // imgCrpr = null;
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
