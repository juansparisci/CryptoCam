using CryptoCam.ViewModel.StatusResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoCam.Views.StatusResult
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessContentView : ContentView
    {
        public SuccessContentView(SuccessContentViewViewModel scvvm)
        {
            InitializeComponent();
            BindingContext = scvvm;
        }
    }
}