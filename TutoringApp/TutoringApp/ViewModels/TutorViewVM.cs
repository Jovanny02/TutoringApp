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
            PictureSize = (DeviceDisplay.MainDisplayInfo.Width * 0.09);
            Radius = PictureSize / 2;

            onPropertyChanged();
        }
        public double PictureSize { get; private set; }
        public double Radius { get; private set; }
        public TutorInfo tutorInfo { get; set; }

    }

}
