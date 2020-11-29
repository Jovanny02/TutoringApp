using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using TutoringApp.Models;
using TutoringApp.Views;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
//using System.Text.Json;
using System.Threading.Tasks;

namespace TutoringApp.ViewModels
{
    public class tutorReservationListVM : BaseVM
    {

        public tutorReservationListVM()
        {
            //TODO Add API call to get  reservations for current user

            tutorSessions.Add(new ReservationTile
            {
                tutorName = "Test Tutor ",
                studentName = "Test Student ",
                zoomLink = "https://zoom.us/",
                fromDate = DateTime.Now.AddHours(-4),
                toDate = DateTime.Now.AddHours(-3),
                tutorUFID = 12345678,
                studentUFID = 87654321,
                isCompleted = true
            });


            for (int i = 0; i < 9; i++)
            {
                tutorSessions.Add(new ReservationTile
                {
                    tutorName = "Test Tutor " + i,
                    studentName = "Test Student " + i,
                    zoomLink = "https://zoom.us/",
                    fromDate = DateTime.Now.AddHours(i - 3),
                    toDate = DateTime.Now.AddHours(i - 2),
                    tutorUFID = 12345678,
                    studentUFID = 87654321
                });

            }

            onPropertyChanged(nameof(isTutorsVisible));
        }

        public ObservableCollection<ReservationTile> tutorSessions { get; set; } = new ObservableCollection<ReservationTile>();

        public bool isTutorsVisible { get { return (tutorSessions.Count > 0); } }

        public bool isTutorTextVisible { get { return !isTutorsVisible; } }

        public ICommand tutorReservationCommand => new Command<object>((selectedItem) =>
        {
            int selectedItemIndex = tutorSessions.IndexOf((ReservationTile)selectedItem);

            ReservationDetails reservationDetails = new ReservationDetails(false, false, tutorSessions[selectedItemIndex].statusMessage);
            reservationDetails.BindingContext = tutorSessions[selectedItemIndex];
            Navigation.PushAsync(reservationDetails);

        });

    }
}
