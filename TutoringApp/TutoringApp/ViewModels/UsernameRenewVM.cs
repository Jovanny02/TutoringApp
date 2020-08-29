using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;
namespace TutoringApp.ViewModels
{
    class UsernameRenewVM:BaseVM
    {
        public UsernameRenewVM()
        {
            UpdateUsernameCommand = new Command(() =>
            {
                //TODO create forgot user_email page
                Console.WriteLine("Triggered updated password");


            });
        }

        public string newpassusername { get; set; }
        public string user_email { get; set; }
        public ICommand UpdateUsernameCommand { protected set; get; }
    }

}
