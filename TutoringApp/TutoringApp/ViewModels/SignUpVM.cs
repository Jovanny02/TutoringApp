using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TutoringApp.Services;
using Acr.UserDialogs;

namespace TutoringApp.ViewModels
{
    class SignUpVM : BaseVM
    {
     
        public SignUpVM()
        {
            

             SignUpCommand = new Command(async
                () =>
            {
                var getSignupDetails = await service.CheckSignupIfExists(userName, password);
                if (getSignupDetails)
                {
                    UserDialogs.Instance.Alert("SignUp", "Sucess", "OK");
                    UserDialogs.Instance.HideLoading();
                }

                else if (userName == null && password == null && email == null && Major == null)
                {
                    UserDialogs.Instance.Alert("Signup failed", "enter all necessay field", "OK");
                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    UserDialogs.Instance.Alert("Failed", "Account Already exist", "OK");
                    UserDialogs.Instance.HideLoading();
                }
                //TODO create sing up call using information in password, user, and email
                Console.WriteLine("Triggered Sign Up Command");
                Navigation.PopAsync();

            });


        }

        public ICommand SignUpCommand { protected set; get; }
        public string Major { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string userName { get; set; }

        APIServices service = new APIServices();



    }
}
