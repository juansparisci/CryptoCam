using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace CryptoCam.ViewModel.StatusResult
{
    public class SuccessContentViewViewModel : INotifyPropertyChanged
    {
        private string description;
        private string result;

        public ImageSource successIconSource { get => ImageSource.FromResource("CryptoCam.Resources.successIcon.png"); }
        public string Description { get => description; set { description = value; OnPropertyChanged(); } }
        public string Result { get => result; set { result = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
