using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TutoringApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using TutoringApp.Services;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SkillDetails : BaseContentPage
    {
        public ICommand deleteCommand;

        public ICommand saveCommand;

        public List<string> depatmentSections = new List<string>();

        
        public SkillDetails(bool isNewSkill)
        {
            
            InitializeComponent();
            initPicker();

            if(isNewSkill)
                titleLabel.Text = "Add Course";

            deleteButton.IsVisible = !isNewSkill;
            //skillSectionPicker.IsEnabled = isNewSkill;                   
        }
        //Work around to set a starting value
        public void initSelectedIndex ()
        {
            skillSectionPicker.SelectedIndex = 0;
        }

        private void initPicker()
        {
            skillSectionPicker.ItemsSource = helperServices.departmentTitles;
        }


        private void save_Clicked(object sender, EventArgs e)
        {
            saveCommand.Execute(sender);
        }

        private void delete_Clicked(object sender, EventArgs e)
        {
            deleteCommand.Execute(sender);
        }

        private void skillSectionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skillSectionPicker.SelectedIndex < 0)
                return;
            CoursePicker.ItemsSource = helperServices.CourseList[skillSectionPicker.SelectedIndex].courses;

            CoursePicker.SelectedIndex = 0;
        }
    }
}