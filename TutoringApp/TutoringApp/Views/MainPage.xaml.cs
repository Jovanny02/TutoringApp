﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TutoringApp.Models;
using TutoringApp.ViewModels;

namespace TutoringApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageVM();
        }

        private void MenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NavigationTile tile = ((NavigationTile)e.SelectedItem);
            if (tile != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(tile.targetType));
                listView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
