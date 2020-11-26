using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorView : BaseContentPage
    {
        private TutorViewVM tutorViewVM;
        public TutorView(object newTutor)
        {
            tutorViewVM = new TutorViewVM(newTutor);
            BindingContext = tutorViewVM;
            tutorViewVM.Navigation = Navigation;
            InitializeComponent();

            double deviceWidthUnits = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
            double deviceHeightUnits = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;

            //set sizing for circular picture
            double pictureSize = deviceWidthUnits * (0.23); //DeviceDisplay.MainDisplayInfo.Width * 0.09;
            double radius = pictureSize / 2;

            tutorPicture.HeightRequest = pictureSize;
            tutorPicture.WidthRequest = pictureSize;
            tutorPicture.CornerRadius = (float)radius;

            reserveButton.HeightRequest = deviceHeightUnits * .1; //10 percent height for button
            reserveButton.WidthRequest = deviceWidthUnits * .4; //40 percent width
            reserveButton.CornerRadius = (int)(deviceHeightUnits * .05); //

            emptyStack.HeightRequest = deviceHeightUnits * .1;

        }
    }
}