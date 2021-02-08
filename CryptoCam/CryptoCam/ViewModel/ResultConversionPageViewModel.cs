using CryptoCam.CustomControls;
using CryptoCam.CustomControls.ActivityIndicator;
using CryptoCam.Model;
using CryptoCam.WebServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
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
        private string total;
        private View activityLoaderPage;

        public string Total { get => total; set { total = value; OnPropertyChanged(); } }
        public bool Loading { get => loading; set  { loading = value; OnPropertyChanged(); } }
        public View ActivityLoaderPage { get => activityLoaderPage; set { activityLoaderPage = value; OnPropertyChanged(); } }

        //       public ImageSource FocusImgSource { get => focusImgSource; set { focusImgSource = value; OnPropertyChanged(); } }

        public  ResultConversionPageViewModel(Stream imgStream, FiatCurrency selectedFiatCurrency, CryptoCurrency selectedCryptoCurrency)
        {
            
            //   FocusImgSource = ImageSource.FromStream(()=>imgStream);
            this.imgStream = imgStream;                     
            this.selectedFiatCurrency = selectedFiatCurrency;
            this.selectedCryptoCurrency = selectedCryptoCurrency;
            ActivityLoaderPage = new TwoArcs();
            Loading = true;
            LoadData();

            CloseCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });
        }

        async void LoadData()
        {
            byte[] imgByteArray;
            using (var memoryStream = new MemoryStream())
            {
                imgStream.CopyTo(memoryStream);
                imgByteArray = memoryStream.ToArray();
            }
            await System.Threading.Tasks.Task.Delay(10000);
            Total = await OCR_API.GetConversion(imgByteArray);
            
            Loading = false;
        }

        public ICommand CloseCommand { protected set; get; }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
