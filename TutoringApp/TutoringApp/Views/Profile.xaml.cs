using System;
using System.Collections.Generic;
using TutoringApp.ViewModels;
using Xamarin.Forms.Xaml;
using SkiaSharp.Views.Forms;
using System.Windows.Input;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : BaseContentPage
    {
        ProfileVM profileVM = new ProfileVM();

        public ICommand EditEducationCommand;
        public Profile()
        {
            EditEducationCommand = profileVM.EditEducationCommand;

            BindingContext = profileVM;
            profileVM.Navigation = Navigation;
            InitializeComponent();

        }

        private void EducationList_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            EditEducationCommand.Execute(e.Item);
        }


    }
}