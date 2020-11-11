using System.Text;
using TutoringApp.Models;
using Xamarin.Essentials;

namespace TutoringApp.ViewModels
{
    class TutorViewVM : BaseVM
    {
        public TutorViewVM(object newTutor)
        {
            tutorInfo = (TutorInfo)newTutor;

        }

        public TutorInfo tutorInfo { get; set; }

    }

}
