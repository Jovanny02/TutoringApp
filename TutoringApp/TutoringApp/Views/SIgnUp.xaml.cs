using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TutoringApp.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Markup;
using dotMorten.Xamarin.Forms;
using TutoringApp.Services;
using System.Collections.Generic;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : BaseContentPage
    {
        SignUpVM signUpVM = new SignUpVM();
        public SignUp(bool isTutor)
        {
            this.isTutor = isTutor;
            BindingContext = signUpVM;
            signUpVM.Navigation = Navigation;
            InitializeComponent();
            signUpVM.isTutor = isTutor;

            CourseSection.IsVisible = this.isTutor;
            ZoomlinkSection.IsVisible = this.isTutor;
            payInfoSection.IsVisible = this.isTutor;

            inputBox.ItemsSource = helperServices.allCourses;
        }

        public bool isTutor = false;

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

                if(searchString == string.Empty)
                {
                    inputBox.ItemsSource = helperServices.allCourses;

                }
                else
                {
                    //Set the ItemsSource to be your filtered dataset
                    inputBox.ItemsSource = suggestionList;
                }
            }
        }

        private void AutoSuggestBox_SuggestionChosen(object sender, dotMorten.Xamarin.Forms.AutoSuggestBoxSuggestionChosenEventArgs e)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            inputBox.Text = e.SelectedItem.ToString();
        }


    }
}