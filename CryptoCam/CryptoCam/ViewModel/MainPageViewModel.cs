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

        private List<FiatCurrency> _FiatCurrencies;
        private List<CryptoCurrency> _CryptoCurrencies;
        private FiatCurrency _SelectedFiatCurrency;
        private CryptoCurrency _SelectedCryptoCurrency;
        private ImageSource _FocusImgSource;
        private string _Result;


        public List<FiatCurrency> FiatCurrencies
        {
            get => _FiatCurrencies;
            set
            {
                _FiatCurrencies = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("FiatCurrency"));
                }
            }

        }
        public List<CryptoCurrency> CryptoCurrencies
        {
            get => _CryptoCurrencies;
            set {
                _CryptoCurrencies = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CryptoCurrencies"));
                }
            }
        }

        public FiatCurrency SelectedFiatCurrency { get => _SelectedFiatCurrency;
            set {
                _SelectedFiatCurrency = value;
                //Check and update exchange rate table 
                Result = "You have selected " + value.Description;
            } }
        public CryptoCurrency SelectedCryptoCurrency { get => _SelectedCryptoCurrency;
            set {
                _SelectedCryptoCurrency = value;
                // check and update exchange rates table
                Result = "You have selected " + value.Description;
            } }
     //   private ConstraintExpression topFrameHeightConstraintExpression;
       //  public ConstraintExpression TopFrameHeightConstraintExpresion { get { return topFrameHeightConstraintExpression; } set { topFrameHeightConstraintExpression = value; OnPropertyChanged(); } }
        public string Result { get => _Result; set { _Result = value; OnPropertyChanged(); } }
        public ImageSource FocusImgSource { get => _FocusImgSource; set { _FocusImgSource = value; OnPropertyChanged(); } }

        // public ImageSource LogoSource { get => ImageSource.FromResource("CryptoCam.Resources.logo_size_invert.jpg"); }

        public MainPageViewModel()
        {
            this.loadCurrencies();
            ScanCommand = new Command(() =>{ this.scan();},() =>{return true;});
            
        }
         
        private void scan()
        {

            var imgBytes = DependencyService.Get<DependencyServices.ICamera>().GetPreviewFromView();
            var mainPage = ((MainPage)Application.Current.MainPage);
            
            Xamarin.Forms.Shapes.Rectangle rect = mainPage.RectangleCameraFocus;
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0,(int)rect.Y,(int)rect.Width,(int)rect.Height);
            System.Drawing.Rectangle sourceRect = new System.Drawing.Rectangle(0,0,(int)mainPage.Width,(int)mainPage.Height);
            
            var imgCrpr = new Helpers.NetStandard.ImageCropper(imgBytes,sourceRect,destRect,Helpers.NetStandard.ImageCropper.Orientation.Portrait);
            FocusImgSource = ImageSource.FromStream(()=>imgCrpr.CroppedImageStream);
            imgCrpr = null;

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
            this._FiatCurrencies = currencies.Item1;
            this._CryptoCurrencies = currencies.Item2;
        }

       
    }
}
