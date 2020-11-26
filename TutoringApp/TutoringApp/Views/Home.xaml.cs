using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringApp.ViewModels;
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
    }
}