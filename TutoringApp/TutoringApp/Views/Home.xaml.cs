using dotMorten.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringApp.ViewModels;
using TutoringApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : BaseContentPage
    {
        HomeVM VM;
        public Home()
        {
            //set naviation to viewmodel
            VM = new HomeVM();
            VM.Navigation = Navigation;
            BindingContext = VM;
            InitializeComponent();

            inputBox.ItemsSource = helperServices.allCourses;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.Current.Properties.ContainsKey("CurrentUser"))//if a user is logged in
            {
                signUpButton.IsVisible = false;
                loginInButton.IsVisible = false;
                SignOutButton.IsVisible = true;
                homeLabel.Text = "Welcome to GatorAid!";
            }

        }

        private void SignOutButton_Clicked(object sender, EventArgs e)
        {
            signUpButton.IsVisible = true;
            loginInButton.IsVisible = true;
            SignOutButton.IsVisible = false;
            homeLabel.Text = "Sign Up or Login";
            VM.SignOutCommand.Execute(sender);
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
                VM.PerformSearchCommand.Execute(inputBox.Text);
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