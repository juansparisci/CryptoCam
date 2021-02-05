﻿using CryptoCam.Model;
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
        private ImageSource focusImgSource;
        private bool loading;
        private string total;
        public string Total { get => total; set { total = value; OnPropertyChanged(); } }
        public bool Loading { get => loading; set  { loading = value; OnPropertyChanged(); } }

        public ImageSource FocusImgSource { get => focusImgSource; set { focusImgSource = value; OnPropertyChanged(); } }

        public  ResultConversionPageViewModel(Stream imgStream, FiatCurrency selectedFiatCurrency, CryptoCurrency selectedCryptoCurrency)
        {
         //   Loading = true;
            this.imgStream = imgStream;
         //   FocusImgSource = ImageSource.FromStream(()=>imgStream);
            
            this.selectedFiatCurrency = selectedFiatCurrency;
            this.selectedCryptoCurrency = selectedCryptoCurrency;
            
            byte[] imgByteArray;
            using (var memoryStream = new MemoryStream())
            {
                imgStream.CopyTo(memoryStream);
                imgByteArray =  memoryStream.ToArray();
            }
           
            CloseCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }, () => { return true; });


                          Total =  OCR_API.GetConversion(imgByteArray);
           
        }
        public ICommand CloseCommand { protected set; get; }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}