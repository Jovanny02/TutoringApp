﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TutoringApp.Models;
using TutoringApp.Services;
namespace TutoringApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            helperServices.setCourseList();
            MainPage = new NavigationPage(new MainPage());

            
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
