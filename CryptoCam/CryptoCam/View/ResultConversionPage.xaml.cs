using CryptoCam.Model;
using CryptoCam.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoCam
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultConversionPage : ContentPage
    {
        ResultConversionPageViewModel rcpViewModel;
        public ResultConversionPage(System.IO.Stream stream, FiatCurrency selectedFiatCurrency, CryptoCurrency selectedCryptoCurrency)
        {
            InitializeComponent(); 
            rcpViewModel= new ResultConversionPageViewModel(stream, selectedFiatCurrency, selectedCryptoCurrency); ;
            BindingContext = rcpViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            rcpViewModel.OnAppearing();
        }

    }
}