using CryptoCam.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CryptoCam
{
    public partial class MainPage : ContentPage
    {
        public Xamarin.Forms.Shapes.Rectangle RectangleCameraFocus { get => rectangleCameraFocus; }
        MainPageViewModel mpViewModel;
        public MainPage()
        {  mpViewModel = new MainPageViewModel();
            InitializeComponent();
            BindingContext = mpViewModel;
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            mpViewModel.OnAppearing();
        }

    }
}
