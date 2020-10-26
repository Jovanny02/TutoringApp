using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TutoringApp.ViewModels
{
    class SignUpVM : BaseVM
    {
        public SignUpVM()
        {
            SignUpCommand = new Command(() =>
            {
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

    }
}
