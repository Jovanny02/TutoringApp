using System;
using System.Windows.Input;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TutoringApp.ViewModels
{
    class HomeVM : BaseVM
    {

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            //TODO create search call
            SearchQuery = query;
            Console.WriteLine(query);
        });

        public string SearchQuery { get; private set; } = "";


    }
}
