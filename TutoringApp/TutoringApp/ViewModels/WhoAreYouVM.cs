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


        }

        public ICommand SignupTutorCommand => new Command((object isTutorString) =>
        {
            //check to see if it is a tutor
            bool isTutor = (isTutorString.ToString() == "True");

            Navigation.PushAsync(new SignUp(isTutor));

        });


    }
}
