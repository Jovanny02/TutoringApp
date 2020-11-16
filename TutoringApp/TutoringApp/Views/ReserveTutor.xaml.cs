using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TutoringApp.ViewModels;
using Xamarin.Essentials;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReserveTutor : BaseContentPage
    {
        private ReserveTutorVM reserveVM { get; set; }
        public ReserveTutor(object tutor)
        {
            InitializeComponent();
            reserveVM = new ReserveTutorVM(tutor);
            BindingContext = reserveVM;


            double deviceHeightUnits = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            calendarStack.HeightRequest = deviceHeightUnits * .35;
            reservationStack.HeightRequest = deviceHeightUnits * .5;
            submitButton.HeightRequest = deviceHeightUnits * .08;

        }
    }
}