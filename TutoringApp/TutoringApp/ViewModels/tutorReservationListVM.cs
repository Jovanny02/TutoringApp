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
            reservationDetails.initiatePayment = initiatePaymentCommand;
            reservationDetails.BindingContext = tutorSessions[selectedItemIndex];
            Navigation.PushAsync(reservationDetails);

        });


        public ICommand initiatePaymentCommand => new Command(async (object selectedReservation) =>
        {
            try
            {
                int indexOfReservation = tutorSessions.IndexOf((ReservationTile)selectedReservation);
                User currTutor = JsonSerializer.Deserialize<User>(App.Current.Properties["CurrentUser"] as string);


                //TODO ADD API CALL TO INITIATE PAYMENT
                UserDialogs.Instance.ShowLoading("Initiaing Payment");
                bool didComplete = StripePaymentService.CreateTransfer(tutorSessions[indexOfReservation].reservationPrice, currTutor.stripeAccountID);
                UserDialogs.Instance.HideLoading();

                if (!didComplete)
                {
                    UserDialogs.Instance.Alert("Get Payment Failed. Please Try Again", null, null);
                    return;
                }

                //UPDATE STATUS IN DB

                bool didSave = await WebAPIServices.setPaymentReceived(tutorSessions[indexOfReservation]);

                if (!didSave)
                {
                    UserDialogs.Instance.Alert("Get Payment Failed. Please Try Again", null, null);
                    return;
                }

                UserDialogs.Instance.Alert("Review Saved Successfully!", null, null);
                //update reservation on users end immediately
                tutorSessions[indexOfReservation].paymentReceived = true;
                onPropertyChanged(nameof(tutorSessions));
                await Navigation.PopAsync();
            }
            catch (Exception e)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Error getting payment", null, null);
                Console.WriteLine(e.Message);
            }

        });



    }
}
