using System.Text;
using TutoringApp.Models;
using Xamarin.Essentials;
using System.Windows.Input;
using Xamarin.Forms;
using TutoringApp.Views;

namespace TutoringApp.ViewModels
{
    class TutorViewVM : BaseVM
    {
        public TutorViewVM(object newTutor)
        {
            tutorInfo = (TutorInfo)newTutor;

        }

        public ICommand reserveCommand => new Command(() => {
            Navigation.PushAsync(new ReserveTutor(tutorInfo));
        });

        public TutorInfo tutorInfo { get; set; }



    }

}
