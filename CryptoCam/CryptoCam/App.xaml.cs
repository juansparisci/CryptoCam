using CryptoCam.DependencyServices.OCR;
using CryptoCam.WebServices;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoCam
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //DependencyService.Register<ICryptoConverter_API, CryptoConverter_API>();
             DependencyService.Register<ICryptoConverter_API, Mock_CryptoConverter_API>();
            DependencyService.Register<IOCR, OCR_RunningOnServer>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
