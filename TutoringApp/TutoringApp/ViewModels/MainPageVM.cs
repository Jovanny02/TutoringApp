﻿using System;
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
            menuTiles.Add(new NavigationTile { pageName = "Settings", iconSrc = "settings.png", targetType = typeof(Settings) });
            menuTiles.Add(new NavigationTile { pageName = "Help", iconSrc = "question.png", targetType = typeof(Help) });
            menuTiles.Add(new NavigationTile { pageName = "Payment", iconSrc = "payment.png", targetType = typeof(Payment) });

            //Used to make picture always be 15% of the screen width
            pictureSize = (DeviceDisplay.MainDisplayInfo.Width * 0.15) ;
            radius = pictureSize / 2;
            Console.WriteLine("Radius: " + radius + "  Size: " + pictureSize);

            //get current user's first name
            if (App.Current.Properties.ContainsKey("CurrentUser"))
            {
                User currUser = JsonSerializer.Deserialize<User>(App.Current.Properties["CurrentUser"] as string);
                //get index of first space so the first name can be substringed
                int index = currUser.name.IndexOf(' ');
                UserMessage = currUser.name.Substring(0, index);
                //TODO add logic to get their picture uri

            }
            else
            {
                UserMessage = "New User";
            }




        }

        private Double pictureSize;
        public Double PictureSize
        {
            get { return pictureSize; }
        }

        private Double radius;
        public Double Radius
        {
            get { return radius; }
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
