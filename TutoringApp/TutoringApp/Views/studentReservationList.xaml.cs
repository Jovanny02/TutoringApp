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
    public partial class studentReservationList : ContentPage
    {
        studentReservationListVM ViewModel;
        public studentReservationList()
        {
            InitializeComponent();

            ViewModel = new studentReservationListVM();
            InitializeComponent();

            this.BindingContext = ViewModel;
            ViewModel.Navigation = Navigation;
        }


        private void Student_ItemTapped(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count < 1 || e.CurrentSelection == null)
                return;

            ViewModel.studentReservationCommand.Execute(e.CurrentSelection[0]);

            ((CollectionView)sender).SelectedItem = null;
        }

    }
}