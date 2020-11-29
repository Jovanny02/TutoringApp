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
    public class studentReservationListVM : BaseVM
    {
        public studentReservationListVM() { 
        //TODO Add API call to get  reservations for current user
            studentSessions.Add(new ReservationTile
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

            for (int i = 0; i < 5; i++)
            {
                studentSessions.Add(new ReservationTile
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

        }

        public ObservableCollection<ReservationTile> studentSessions { get; set; } = new ObservableCollection<ReservationTile>();

        public bool isStudentsVisible { get { return (studentSessions.Count > 0); } }

        public ICommand studentReservationCommand => new Command<object>((selectedItem) =>
        {
            int selectedItemIndex = studentSessions.IndexOf((ReservationTile)selectedItem);

            bool showSubmit = false;
            //if the reservation is not cancelled, not complete, and the end time has passed, allow the showing of the submission prompt
            if (!studentSessions[selectedItemIndex].isCanceled && !studentSessions[selectedItemIndex].isCompleted && studentSessions[selectedItemIndex].toDate < DateTime.Now)
                showSubmit = true;

            ReservationDetails reservationDetails = new ReservationDetails(true, showSubmit, studentSessions[selectedItemIndex].statusMessage);
            reservationDetails.BindingContext = studentSessions[selectedItemIndex];
            reservationDetails.submitRatingCommand = submitRatingCommand;
            Navigation.PushAsync(reservationDetails);

        });

        //command to submit a user rating 
        public ICommand submitRatingCommand => new Command(async (object selectedReservation) =>
        {
            try
            {
                int indexOfReservation = studentSessions.IndexOf((ReservationTile)selectedReservation);


                //TODO ADD API CALL TO SUBMIT RATING
                UserDialogs.Instance.ShowLoading("Submitting Review");
                await Task.Delay(TimeSpan.FromSeconds(2));
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Review Saved Successfully!", null, null);


                //update reservation on users end immediately
                studentSessions[indexOfReservation].isCompleted = true;
                onPropertyChanged(nameof(studentSessions));
                await Navigation.PopAsync();
            }
            catch (Exception e)
            {
                UserDialogs.Instance.Alert("Error submitting rating", null, null);
                Console.WriteLine(e.Message);
            }
        });


    }
}
