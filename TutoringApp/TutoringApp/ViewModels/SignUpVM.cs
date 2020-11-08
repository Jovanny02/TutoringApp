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
                //TODO create sing up call using information in password, user, and email
                Console.WriteLine("Triggered Sign Up Command");
                var isSucess = await service.LogininSucess(userName, password, email);
                if (isSucess)
                
                    {
                        UserDialogs.Instance.Alert("Sucess", "Sucess", "OK");
                        UserDialogs.Instance.HideLoading();
                    }
                else
                    {

                        UserDialogs.Instance.HideLoading();
                        UserDialogs.Instance.Alert("something went wrong", "Payment failed", "Ok");

                    }
                
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
