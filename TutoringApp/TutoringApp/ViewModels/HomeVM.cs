using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace TutoringApp.ViewModels
{
    class HomeVM : BaseVM
    {
        public ICommand PerformSearchCommand { protected set; get; }

        private string searchQuery { get; set; }
        public string SearchQuery 
        { 
            get { return searchQuery; }
            set { searchQuery = value; onPropertyChanged(); } //needed for binded Items to see changes appear on view
        }
         
        public HomeVM()
        {
            PerformSearchCommand = new Command<string> ((string query) =>
            {
                //TODO create search call
                SearchQuery = query;
            });


        }

    }
}
