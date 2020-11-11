using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TutoringApp.Models;
using TutoringApp.ViewModels;
using TutoringApp.Views;
using Xamarin.Essentials;
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

            pictureSize = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) * (.5); //DeviceDisplay.MainDisplayInfo.Width * 0.09;
            radius = pictureSize / 2;

            userPicture.HeightRequest = pictureSize;
            userPicture.WidthRequest = pictureSize;
            userPicture.CornerRadius = (float)radius;

        }

        public Double pictureSize { get; set; }
        public Double radius { get; set; }

        private void MenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NavigationTile tile = ((NavigationTile)e.SelectedItem);
            if (tile != null)
            {
                //set detail page to selected item from menu 
                Detail = new NavigationPage((Page)Activator.CreateInstance(tile.targetType));

                //deselect menu item and set menu presentation to false
                listView.SelectedItem = null;
                IsPresented = false;
            }
        }


        public void NavigateToHelp()
        {
            //TODO: NAVIGATES TO CREDITS PAGE. MUST CHANGE
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Credits)));
        }
    }
}
