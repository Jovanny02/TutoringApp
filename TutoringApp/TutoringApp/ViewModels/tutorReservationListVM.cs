using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using TutoringApp.Models;
using TutoringApp.Views;
using System.Windows.Input;
using Xamarin.Forms;
using TutoringApp.Services;
using System.Text.Json;
using Acr.UserDialogs;
//using System.Text.Json;
using System.Threading.Tasks;

namespace TutoringApp.ViewModels
{
    public class tutorReservationListVM : BaseVM
    {

        public tutorReservationListVM()
        {
            User currentTutor = new User();

            UserDialogs.Instance.ShowLoading();
            PerformReservationCommand.Execute(currentTutor);
            UserDialogs.Instance.HideLoading();


            onPropertyChanged(nameof(isTutorsVisible));
        }

        public ICommand PerformReservationCommand => new Command<User>(async (currentTutor) =>
        {

            currentTutor = JsonSerializer.Deserialize<User>(App.Current.Properties["CurrentUser"] as string);
            List<ReservationTile> tutorReservation = await WebAPIServices.getTutorReservationTiles(currentTutor.UFID);
            if (tutorReservation != null)
            {
                tutorSessions = new ObservableCollection<ReservationTile>(tutorReservation);
            }
            else
            {
                tutorSessions = null;
            }

            //properties changed
            onPropertyChanged(nameof(tutorSessions));
            onPropertyChanged(nameof(isTutorsVisible));
        });



        public ObservableCollection<ReservationTile> tutorSessions { get; set; } = new ObservableCollection<ReservationTile>();

        public bool isTutorsVisible { 
            get {
                if (tutorSessions == null)
                    return false;


                return (tutorSessions.Count > 0); 
            } 
        }

        public ICommand tutorReservationCommand => new Command<object>((selectedItem) =>
        {
            int selectedItemIndex = tutorSessions.IndexOf((ReservationTile)selectedItem);

            ReservationDetails reservationDetails = new ReservationDetails(false, false, tutorSessions[selectedItemIndex].statusMessage);
            reservationDetails.BindingContext = tutorSessions[selectedItemIndex];
            Navigation.PushAsync(reservationDetails);

        });

    }
}
