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
    public partial class ReservationList : BaseContentPage
    {
        private ReservationListVM ViewModel;
        public ReservationList()
        {
            ViewModel = new ReservationListVM();
            InitializeComponent();

            this.BindingContext = ViewModel;
            ViewModel.Navigation = Navigation;
        }

        private void Tutor_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null || e.ItemIndex < 0)
                return;

            ViewModel.tutorReservationCommand.Execute(e.ItemIndex);

        }

        private void Student_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null || e.ItemIndex < 0)
                return;

            ViewModel.studentReservationCommand.Execute(e.ItemIndex);

        }

    }
}