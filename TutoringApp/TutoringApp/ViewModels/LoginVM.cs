using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;
using TutoringApp.Views;

namespace TutoringApp.ViewModels
{
    class LoginVM : BaseVM
    {
        public LoginVM()
        {
            LoginCommand = new Command(() =>
            {
                //TODO create login call using information in password and user_email
                Console.WriteLine("Triggered Login Command");
                Navigation.PopAsync();
            });

            ForgotUserCommand = new Command(() =>
            {
                Navigation.PushAsync(new UsernameRenew());

                //TODO create forgot user_email page
                //Console.WriteLine("Triggered forgot user name command");

            });

            ForgotPassCommand = new Command(() =>
            {
                 Navigation.PushAsync(new PaswordRenew());
                //TODO create forgot password page
                //Console.WriteLine("Triggered forgot password command");
            });
        }


        public string password { get; set; }
        public string user_email { get; set; }
        public ICommand LoginCommand { protected set; get; }
        public ICommand ForgotUserCommand { protected set; get; }

        public ICommand ForgotPassCommand { protected set; get; }
    }




}