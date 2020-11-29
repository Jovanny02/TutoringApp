using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;
using TutoringApp.Views;
using TutoringApp.Services;
using Acr.UserDialogs;

namespace TutoringApp.ViewModels
{
    class LoginVM : BaseVM
    {
        public LoginVM()
        {
            LoginCommand = new Command(async () =>
            {
                //TODO create login call using information in password and user_email
                Console.WriteLine("Triggered Login Command");
                Navigation.PopAsync();
                var getLoginDetails = await service.CheckLoginIfExists(username, password);
                if (getLoginDetails)
                {
                    UserDialogs.Instance.Alert("Sucess", "Sucess", "OK");
                    UserDialogs.Instance.HideLoading(); 
                }

                // education page for cheking page
                else if(username == null || password== null)
                {
                    UserDialogs.Instance.Alert("Login failed", "Enter username and password", "OK");
                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    UserDialogs.Instance.Alert("Login failed", "Username or password incorrect", "OK");
                    UserDialogs.Instance.HideLoading();
                }



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
        public string username { get; set; }
        public ICommand LoginCommand { protected set; get; }
        public ICommand ForgotUserCommand { protected set; get; }

        public ICommand ForgotPassCommand { protected set; get; }

        APIServices service = new APIServices();
    }




}