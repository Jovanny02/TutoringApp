using System;
using Xamarin.Forms;
using System.Collections.Generic;
using TutoringApp.ViewModels;
using Xamarin.Forms.Xaml;
using SkiaSharp.Views.Forms;
using System.Windows.Input;
using System.Threading.Tasks;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : BaseContentPage
    {
        ProfileVM profileVM = new ProfileVM();

        public ICommand EditEducationCommand;

        public ICommand EditSkillCommand;
        public Profile()
        {
            EditEducationCommand = profileVM.EditEducationCommand;
            EditSkillCommand = profileVM.EditSkillCommand;

            BindingContext = profileVM;
            profileVM.Navigation = Navigation;
            InitializeComponent();

        }

        private void EducationList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            EditEducationCommand.Execute(e.Item);
        }


        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            EditSkillCommand.Execute(e.Item);

            skillsList.SelectedItem = null;

        }
    }
}