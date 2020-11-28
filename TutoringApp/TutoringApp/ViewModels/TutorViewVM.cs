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
            courseListHeight = tutorInfo.Courses.Count * 45;
            educationListHeight = tutorInfo.EducationSections.Count * 90;
        }

        public ICommand reserveCommand => new Command(() => {
            //only allow reservation if user is logged in
            if (App.Current.Properties.ContainsKey("CurrentUser"))
            {
                Navigation.PushAsync(new ReserveTutor(tutorInfo));
            }
            else
            {
                Navigation.PushAsync(new Login());
            }

        });

        public ICommand PerformSearchCommand => new Command<string>((string query) =>
        {
            //TODO create search call
            //navigate to search page

        });

        public TutorInfo tutorInfo { get; set; }

        public int courseListHeight { get; set; }

        public int educationListHeight { get; set; }

    }

}
