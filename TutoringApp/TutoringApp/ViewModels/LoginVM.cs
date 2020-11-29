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
                //check values
                if(string.IsNullOrWhiteSpace(UFID) || string.IsNullOrWhiteSpace(password))
                {
                    UserDialogs.Instance.Alert("UFID and password cannot be empty", null, null);
                    return;
                }



                //attempt login
                UserDialogs.Instance.ShowLoading("Logging In");
                bool didLogin = false;
                try
                {
                     didLogin = await WebAPIServices.checkLoginCredentials(UFID.Replace(" ", string.Empty), password.Replace(" ", string.Empty));
                }
                catch(Exception e)
                {
                    UserDialogs.Instance.Alert("Error:" + e.Message, null, null);
                    UserDialogs.Instance.HideLoading();
                    return;
                }

                //validate login result
                UserDialogs.Instance.HideLoading();
                if (didLogin)
                {
                    UserDialogs.Instance.Alert("Logged in successfully!", null, null);
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    UserDialogs.Instance.Alert("Login Failed: Please enter a valid UFID and password", null, null);

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


        public string password { get; set; } = "";
        public string UFID { get; set; } = "";
        public ICommand LoginCommand { protected set; get; }
        public ICommand ForgotUserCommand { protected set; get; }

        public ICommand ForgotPassCommand { protected set; get; }
    }




}