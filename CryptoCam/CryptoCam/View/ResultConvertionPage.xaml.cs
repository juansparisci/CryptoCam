using CryptoCam.Model;
using CryptoCam.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoCam
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultConvertionPage : ContentPage
    {
        ResultConvertionPageViewModel rcpViewModel;
        public ResultConvertionPage(string fiatAmount, string cryptoAmount, FiatCurrency selectedFiatCurrency, CryptoCurrency selectedCryptoCurrency)
        {
            InitializeComponent(); 
            rcpViewModel= new ResultConvertionPageViewModel(fiatAmount,cryptoAmount, selectedFiatCurrency, selectedCryptoCurrency); ;
            BindingContext = rcpViewModel;
        }
        public ResultConvertionPage(string error)
        {
            InitializeComponent();
            rcpViewModel = new ResultConvertionPageViewModel(error); ;
            BindingContext = rcpViewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            rcpViewModel.OnAppearing();
        }

    }
}