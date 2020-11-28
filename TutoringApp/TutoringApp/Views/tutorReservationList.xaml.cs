using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TutoringApp.ViewModels;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class tutorReservationList : ContentPage
    {

        private tutorReservationListVM ViewModel;

        public tutorReservationList()
        {
            InitializeComponent();

            ViewModel = new tutorReservationListVM();
            InitializeComponent();

            this.BindingContext = ViewModel;
            ViewModel.Navigation = Navigation;
        }

        private void Tutor_ItemTapped(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count < 1 || e.CurrentSelection == null )
                return;

            ViewModel.tutorReservationCommand.Execute(e.CurrentSelection[0]);

            ((CollectionView)sender).SelectedItem = null;
        }

    }
}