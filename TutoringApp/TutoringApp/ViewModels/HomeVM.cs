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
            PerformSearchCommand = new Command<string>((string query) =>
            {
                //TODO create search call
                SearchQuery = query;
                //navigate to search page
                //Navigation.PushAsync(new TutorList(query));


            });

            //Commands to handle button click for login and signup
            //TODO: change to match correct page
            SignUpCommand = new Command(() => {
               // Navigation.PushAsync(new SignUp());



            });

            LoginCommand = new Command(() => {
                Navigation.PushAsync(new Login());
            });

        }



        public ICommand PerformSearchCommand { protected set; get; }
        public ICommand SignUpCommand { protected set; get; }

        public ICommand LoginCommand { protected set; get; }
        private string searchQuery { get; set; }
        public string SearchQuery
        {
            get { return searchQuery; }
            set { searchQuery = value; onPropertyChanged(); } //needed for binded Items to see changes appear on view
        }




    }
}
