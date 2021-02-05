﻿using CryptoCam.ViewModel;
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
        
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            
        }

       
    }
}