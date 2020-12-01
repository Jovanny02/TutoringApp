using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringApp.ViewModels;
using TutoringApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using dotMorten.Xamarin.Forms;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorList : BaseContentPage
    {
        TutorListVM pageVM;
        public TutorList(string searchParam)
        {
            pageVM = new TutorListVM(searchParam);
            BindingContext = pageVM;
            InitializeComponent();

            //set sizing for circular picture
            pictureSize = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) * (0.1);
            radius = pictureSize / 2;

            //initialize search
            List<string> suggestionList = new List<string>();
            suggestionList.Add(searchParam);
            inputBox.ItemsSource = suggestionList;
            inputBox.Text = searchParam;
        }

        public Double pictureSize { get; set; }
        public int EditLabelSize { get; set; }
        public Double radius { get; set; }


        private void TutorSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count < 1 || e.CurrentSelection[0] == null)
                return;
            ListedTutors.SelectedItem = null;

            //if a user is not logged in
            if (!App.Current.Properties.ContainsKey("CurrentUser"))
            {
                //navigate to login instead
                Navigation.PushAsync(new Login());
                return;
            }

            Navigation.PushAsync(new TutorView(e.CurrentSelection[0]));
        }

        private void AutoSuggestBox_TextChanged(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxTextChangedEventArgs e)
        {
            if (e.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                List<string> suggestionList = new List<string>();
                string searchString = inputBox.Text.ToUpper().Replace(" ", string.Empty);

                foreach (string course in helperServices.allCourses)
                {
                    if (course.StartsWith(searchString))
                        suggestionList.Add(course);
                }

                //Set the ItemsSource to be your filtered dataset
                inputBox.ItemsSource = suggestionList;
            }
        }

        private void AutoSuggestBox_QuerySubmitted(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxQuerySubmittedEventArgs e)
        {
            if (e.ChosenSuggestion != null)
            {
                // User selected an item from the suggestion list, take an action on it here.
                pageVM.PerformSearchCommand.Execute(inputBox.Text);
                List<string> suggestionList = new List<string>();
                suggestionList.Add(inputBox.Text);

                inputBox.ItemsSource = suggestionList;
            }
            else
            {
                // User hit Enter from the search box. Use args.QueryText to determine what to do.
                inputBox.Text = "";
            }
        }

        private void AutoSuggestBox_SuggestionChosen(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxSuggestionChosenEventArgs e)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            inputBox.Text = e.SelectedItem.ToString();
        }



    }
}