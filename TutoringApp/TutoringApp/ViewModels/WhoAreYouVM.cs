using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;
using TutoringApp.Views;

namespace TutoringApp.ViewModels
{
   class WhoAreYouVM: BaseVM
    {
        
        public WhoAreYouVM() 
        {
            SignupTutorCommand= new Command(() =>
            {
                Navigation.PushAsync(new SignUp());

                //TODO create forgot user_email page
                //Console.WriteLine("Triggered forgot user name command");

            });

        }

        public ICommand SignupTutorCommand { protected set; get; }


    }
}
