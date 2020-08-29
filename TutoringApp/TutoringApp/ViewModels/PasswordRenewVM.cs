using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.ViewModels
{
    class PasswordRenewVM: BaseVM
    {
        public PasswordRenewVM()
        {
            UpdatePassCommand = new Command(() =>
            {
                //TODO create forgot user_email page
                Console.WriteLine("Triggered updated password");


            });

        }
        public string newpassword { get; set; }
        public string user_email { get; set; }     
        public ICommand UpdatePassCommand { protected set; get; }
    }
}
