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
    public class ResultConversionPageViewModel : INotifyPropertyChanged
    {
        private Stream imgStream;
        private FiatCurrency selectedFiatCurrency;
        private CryptoCurrency selectedCryptoCurrency;
    //    private ImageSource focusImgSource;
        private bool loading;
        private ArcsBase activityLoaderPage;
        private ContentPage loadingContentPage;
        private ContentView resultContentView;

        

        public bool Loading { get => loading; 
            set  { 
                loading = value;
                if (value)
                {
                    Application.Current.MainPage.Navigation.PushModalAsync(this.loadingContentPage,false);
                }
                else
                {
                    Application.Current.MainPage.Navigation.PopModalAsync(false);
                }
                OnPropertyChanged();
                
            } }

        public ArcsBase ActivityLoaderPage { get => activityLoaderPage; set { activityLoaderPage = value; OnPropertyChanged(); } }

        //       public ImageSource FocusImgSource { get => focusImgSource; set { focusImgSource = value; OnPropertyChanged(); } }

        public  ResultConversionPageViewModel(Stream imgStream, FiatCurrency selectedFiatCurrency, CryptoCurrency selectedCryptoCurrency)
        {
            

            //   FocusImgSource = ImageSource.FromStream(()=>imgStream);
            

            this.imgStream = imgStream;                     
            this.selectedFiatCurrency = selectedFiatCurrency;
            this.selectedCryptoCurrency = selectedCryptoCurrency;
         //   ActivityLoaderPage = new FourArcs(new LoadingText(selectedFiatCurrency.Description+"->"+ selectedCryptoCurrency.Description,new CenterTextPosition()));
         //     Loading = true;
            // LoadData();


            this.loadingContentPage = new ContentPage { Content = new FourArcs(new LoadingText(selectedFiatCurrency.Description + "->" + selectedCryptoCurrency.Description, new CenterTextPosition())) };

            CloseCommand = new Command(async () =>
            {
              await Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });
        }

        async Task<bool> LoadData()
        {
         
                byte[] imgByteArray;
                using (var memoryStream = new MemoryStream())
                {
                    imgStream.CopyTo(memoryStream);
                    imgByteArray = memoryStream.ToArray();
                }
                //  await System.Threading.Tasks.Task.Delay(10000);
                var amountReaded = await DependencyService.Get<IOCR>()?.GetTextFromImage(imgByteArray);
                var cryptoEquivalent = await DependencyService.Get<ICryptoConverter_API>()?.Convert(Convert.ToDecimal(amountReaded), selectedCryptoCurrency.Id, selectedFiatCurrency.Id);

                ResultContentView = new Views.StatusResult.SuccessContentView( 
                                            new SuccessContentViewViewModel
                                                { 
                                                    Description = amountReaded+" "+selectedFiatCurrency.Description, 
                                                    Result = cryptoEquivalent+" "+selectedCryptoCurrency.Description 
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
            if (ResultContentView == null)//String.IsNullOrWhiteSpace(Total)&& String.IsNullOrWhiteSpace(Error))
            {
                try
                {      
       

                    Loading = true;
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
                finally
                {
                    if(Loading)Loading= false;
                }                
            }

        }
    }
}
