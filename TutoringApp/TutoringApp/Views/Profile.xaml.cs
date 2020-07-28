using System;
using System.Collections.Generic;
using TutoringApp.ViewModels;
using Xamarin.Forms.Xaml;
using SkiaSharp.Views.Forms;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : BaseContentPage
    {
        ProfileVM profileVM = new ProfileVM();
        public Profile()
        {
            BindingContext = profileVM;
            profileVM.Navigation = Navigation;
            InitializeComponent();

        }
    }
}