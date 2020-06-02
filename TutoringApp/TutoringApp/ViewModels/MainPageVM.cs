using System;
using System.Collections.Generic;
using System.Text;
using TutoringApp.Models;
using TutoringApp.Views;
using Xamarin.Forms;

namespace TutoringApp.ViewModels
{
    class MainPageVM : BaseVM
    {
        public MainPageVM()
        {
            NavigationTile homeTile = new NavigationTile { pageName = "Home", iconSrc = "home.png", targetType = typeof(Home) };
            NavigationTile resumeTile = new NavigationTile { pageName = "Jovanny's Resume", iconSrc = "resume.png", targetType = typeof(Resume) };
            NavigationTile creditsTile = new NavigationTile { pageName = "Credits", iconSrc = "Credits.png", targetType = typeof(Credits) };
            menuTiles = new List<NavigationTile>();

            menuTiles.Add(homeTile);
            menuTiles.Add(resumeTile);
            menuTiles.Add(creditsTile);
        }

        private List<NavigationTile> menuTiles;
        public List<NavigationTile> MenuTiles
        {
            get { return menuTiles; }    
        }

    }
}
