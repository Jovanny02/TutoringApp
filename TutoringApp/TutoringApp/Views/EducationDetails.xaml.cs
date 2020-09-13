using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;
using System;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EducationDetails : BaseContentPage
    {
        public EducationDetails()
        {
            InitializeComponent();

            for (int i = DateTime.Now.Year + 4; i >= 1980; i--)
            {
                toYears.Add(i);
                fromYears.Add(i);
            }

            //set year picker lists
            toYearPicker.ItemsSource = toYears;

            fromYears.RemoveRange(0, 4);
            fromYearPicker.ItemsSource = fromYears;

        }


        public List<int> toYears = new List<int>();
        public List<int> fromYears = new List<int>();
        public ICommand SaveCommand;
        public ICommand deleteCommand;

        public void hideDelete () {
            deleteButton.IsVisible = false;
        }

        //save button pressed
        private void Save_Clicked(object sender, EventArgs e)
        {
            //checks for incorrect inputs
            if(universityEntry.Text == "" || universityEntry.Text == null)
            {
                errorLabel.Text = "University name can not be empty";
                return;
            }
            else if (majorEntry.Text == "" || majorEntry.Text == null)
            {
                errorLabel.Text = "Major can not be empty";
                return;
            }
            else if((int)fromYearPicker.SelectedItem > (int)toYearPicker.SelectedItem)
            {
                errorLabel.Text = "From year can not be greater than to year";
                return;
            }

            SaveCommand.Execute(sender);
        }

        private void delete_Clicked(object sender, EventArgs e)
        {
            deleteCommand.Execute(sender);
        }
    }
}