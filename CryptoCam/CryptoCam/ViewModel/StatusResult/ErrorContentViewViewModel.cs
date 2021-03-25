using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace CryptoCam.ViewModel.StatusResult
{
    public class ErrorContentViewViewModel : INotifyPropertyChanged
    {
        private string shortDescription;
        private string longDescription;
        public ImageSource errorIconSource { get => ImageSource.FromResource("CryptoCam.Resources.errorIcon.png"); }
        public string ShortDescription { get => shortDescription; set { shortDescription = value; OnPropertyChanged(); } }
        public string LongDescription { get => longDescription; set {longDescription = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
