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
                set{
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
                Result = "You have selected "+value.Description;
            } }
        public CryptoCurrency SelectedCryptoCurrency { get => selectedCryptoCurrency; 
            set { 
                selectedCryptoCurrency = value;
                // check and update exchange rates table
                Result = "You have selected " + value.Description;
            } }
        public string Result { get => result; set { result = value; OnPropertyChanged(); } }

        public MainPageViewModel()
        {
            this.loadCurrencies();
            ScanCommand = new Command(() =>{ this.scan();},() =>{return true;});
            
        }
         
        private void scan()
        {
            Result = "ScanCommand Pressed";
            var imgBytes = DependencyService.Get<DependencyServices.ICamera>().GetPreviewFromView();

            

            var r = WebServices.OCR_API.GetTextFromImage(imgBytes);

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
