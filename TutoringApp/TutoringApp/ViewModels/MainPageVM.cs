using System;
using System.Collections.Generic;
using System.Text;
using TutoringApp.Models;
using TutoringApp.Views;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Text.Json;

namespace TutoringApp.ViewModels
{
    class MainPageVM : BaseVM
    {

        public MainPageVM()
        {

            menuTiles.Add(new NavigationTile { pageName = "Profile", iconSrc = "user.png", targetType = typeof(Profile) });
            menuTiles.Add(new NavigationTile { pageName = "Home", iconSrc = "home.png", targetType = typeof(Home) });
            menuTiles.Add(new NavigationTile { pageName = "Reservations", iconSrc = "calendar.png", targetType = typeof(ReservationList) });
            //menuTiles.Add(new NavigationTile { pageName = "Settings", iconSrc = "settings.png", targetType = typeof(Settings) });
            menuTiles.Add(new NavigationTile { pageName = "Help", iconSrc = "question.png", targetType = typeof(Help) });

            checkUserStatus();
        }


        public void checkUserStatus ()
        {
            //get current user's first name
            if (App.Current.Properties.ContainsKey("CurrentUser"))
            {
                User currUser = JsonSerializer.Deserialize<User>(App.Current.Properties["CurrentUser"] as string);
                //get index of first space so the first name can be substringed
                int index = currUser.name.IndexOf(' ');
                //TODO add logic to get their picture uri
                if(currUser.pictureSrc != pictureSrc || UserMessage != currUser.name.Substring(0, index))
                {
                    pictureSrc = currUser.pictureSrc;
                    UserMessage = currUser.name.Substring(0, index);
                    onPropertyChanged(nameof(userMessage));
                    onPropertyChanged(nameof(pictureSrc));
                }

            }
            else
            {
                if(pictureSrc != "user.png" || UserMessage != "New User")
                {
                    UserMessage = "New User";
                    pictureSrc = "user.png";
                    onPropertyChanged(nameof(userMessage));
                    onPropertyChanged(nameof(pictureSrc));
                }


            }


        }



        private List<NavigationTile> menuTiles = new List<NavigationTile>();
        //Set in VM for binding in view
        public List<NavigationTile> MenuTiles
        {
            get { return menuTiles; }    
        }

        public string pictureSrc { get; private set; } = "user.png";

        private string UserMessage { get;  set; }
        public string userMessage { get { return "Hello " + UserMessage + "!"; }} 

    }
}
