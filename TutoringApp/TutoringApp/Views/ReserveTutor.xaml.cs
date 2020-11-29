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
            reserveVM.Navigation = this.Navigation;

            double deviceHeightUnits = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;
            calendarStack.HeightRequest = deviceHeightUnits * .35;
            submitButton.HeightRequest = deviceHeightUnits * .08;

        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection == null | e.CurrentSelection.Count < 1)
                return;

            reserveVM.handleTappedReservation(e.CurrentSelection[0]);

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}