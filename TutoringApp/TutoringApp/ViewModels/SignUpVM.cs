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
                //TODO create sign up call using information in password, user, and email

                //pop twice to get to home page
                Navigation.PopAsync();
                Navigation.PopAsync();

            });


        }

        public ICommand SignUpCommand { protected set; get; }
        public string Course { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public string UFID { get; set; }


    }
}
