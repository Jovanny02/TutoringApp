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
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            mainPageVM = new MainPageVM();
            BindingContext = mainPageVM;
            this.IsPresentedChanged += checkUser;
            double thisWidth = this.Master.Width;
            pictureSize = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) * (.5); //DeviceDisplay.MainDisplayInfo.Width * 0.09;
            radius = pictureSize / 2;

            userPicture.HeightRequest = pictureSize;
            userPicture.WidthRequest = pictureSize;
            userPicture.CornerRadius = (float)radius;

        }

        private MainPageVM mainPageVM;
        public Double pictureSize { get; set; }
        public Double radius { get; set; }

        private void SignOutButton_Clicked(object sender, EventArgs e)
        {
            //SignOutButton.IsVisible = false;
            mainPageVM.SignOutCommand.Execute(sender);
            checkUser("", new EventArgs());
        }

        private void MenuItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection == null || e.CurrentSelection.Count < 1)
                return;

            NavigationTile tile = ((NavigationTile)e.CurrentSelection[0]);
            if (tile != null)
            {
                if((tile.targetType == typeof(Profile) || tile.targetType == typeof(TabbedReservationList)  )&& !App.Current.Properties.ContainsKey("CurrentUser"))
                {
                    Navigation.PushAsync(new Login()); // redirect to login if not logged in and page is profile
                                                       //deselect menu item and set menu presentation to false
                    menuCollection.SelectedItem = null;
                    IsPresented = false;
                }
                else
                {

                   

                if (tile.targetType == typeof(TabbedReservationList))
                {
                    Detail = (Page)Activator.CreateInstance(tile.targetType);

                }
                else
                {
                    //set detail page to selected item from menu 
                    Detail = new NavigationPage((Page)Activator.CreateInstance(tile.targetType));
                }


                    //deselect menu item and set menu presentation to false
                    menuCollection.SelectedItem = null;
                    IsPresented = false;
                }
            }
        }
        private void checkUser(object sender, EventArgs e)
        {

            if(pictureSize > this.Master.Width)
            {
                pictureSize = this.Master.Width * .75;
                radius = pictureSize / 2;

                userPicture.HeightRequest = pictureSize;
                userPicture.WidthRequest = pictureSize;
                userPicture.CornerRadius = (float)radius;
            }

            //check and update user picture and message
            // SignOutButton.IsVisible = mainPageVM.checkUserStatus();
            mainPageVM.checkUserStatus();
        }

        public void NavigateToHelp()
        {
            //TODO: NAVIGATES TO CREDITS PAGE. MUST CHANGE
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Credits)));
        }

    }
}
