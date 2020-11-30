using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using TutoringApp.Views;

namespace TutoringApp.ViewModels
{
    class HomeVM : BaseVM
    {

        public HomeVM()
        {


        }



        public ICommand PerformSearchCommand => new Command<string>((string query) =>
        {
            //TODO create search call
            SearchQuery = query;

            //navigate to search page
            Navigation.PushAsync(new TutorList(query));

        });
        public ICommand SignUpCommand => new Command(() => {
            Navigation.PushAsync(new WhoAreYou());
        });

        public ICommand LoginCommand => new Command(() => {
            Navigation.PushAsync(new Login());
        });

        public ICommand SignOutCommand => new Command(() => {
            if (App.Current.Properties.ContainsKey("CurrentUser"))//if a user is logged in
            {
                //delete the user information (logout)
                App.Current.Properties.Remove("CurrentUser");
            }

        });

        private string searchQuery { get; set; }
        public string SearchQuery
        {
            get { return searchQuery; }
            set { searchQuery = value; onPropertyChanged(); } //needed for binded Items to see changes appear on view
        }




    }
}
