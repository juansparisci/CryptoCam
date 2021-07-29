using CryptoCam.CustomControls;
using CryptoCam.CustomControls.ActivityIndicator;
using CryptoCam.DependencyServices.OCR;
using CryptoCam.Model;
using CryptoCam.ViewModel.StatusResult;
using CryptoCam.WebServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CryptoCam.ViewModel
{
    public class ResultConvertionPageViewModel : INotifyPropertyChanged
    {
        private Stream imgStream;
        private FiatCurrency selectedFiatCurrency;
        private CryptoCurrency selectedCryptoCurrency;
        private ImageSource focusImgSource;

        private ContentView resultContentView;
        private string fiatAmount;
        private string cryptoAmount;





        //      public ImageSource FocusImgSource { get => focusImgSource; set { focusImgSource = value; OnPropertyChanged(); } }

        public  ResultConvertionPageViewModel(string fiatAmount, string cryptoAmount, FiatCurrency selectedFiatCurrency, CryptoCurrency selectedCryptoCurrency)
        {
            this.fiatAmount = fiatAmount;
            this.cryptoAmount = cryptoAmount;

                
            this.selectedFiatCurrency = selectedFiatCurrency;
            this.selectedCryptoCurrency = selectedCryptoCurrency;


            

            CloseCommand = new Command(async () =>
            {
              await Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });
        }

        public ResultConvertionPageViewModel(string error)
        {
            ResultContentView = new Views.StatusResult.ErrorContentView(
                                 new ErrorContentViewViewModel
                                 {
                                     ShortDescription = "",
                                     LongDescription = error
                                 }
                         );

            CloseCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });
        }

        async Task<bool> LoadData()
        {
           
              
                ResultContentView = new Views.StatusResult.SuccessContentView( 
                                            new SuccessContentViewViewModel
                                                { 
                                                    Description = selectedFiatCurrency.Description + " " + this.fiatAmount, 
                                                    Result = selectedCryptoCurrency.Description + " " + this.cryptoAmount
                                            });

            return true;
            

        }

        public ICommand CloseCommand { protected set; get; }
        public ContentView ResultContentView { get => resultContentView; set { resultContentView = value; OnPropertyChanged(); } }

        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public async void OnAppearing()
        {            
            if ( ResultContentView == null)
            {
                try
                {      
                    await this.LoadData();
                }
                catch (Exception ex)
                {
                     
                    ResultContentView = new Views.StatusResult.ErrorContentView(
                                new ErrorContentViewViewModel { 
                                     ShortDescription="",
                                     LongDescription = ex.Message
                                }
                        );
                }
            }

        }
    }
}
