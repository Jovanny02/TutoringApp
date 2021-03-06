﻿using System;
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
using TutoringApp.Services;
using System.Text.Json;

namespace TutoringApp.ViewModels
{
    public class studentReservationListVM : BaseVM
    {
        public studentReservationListVM() {
            User student = new User();
            UserDialogs.Instance.ShowLoading();
            PerformReservationCommand.Execute(student);
            UserDialogs.Instance.HideLoading();

        }
        //TODO Add API call to get  reservations for current user
        public ICommand PerformReservationCommand => new Command<User>(async (student) =>
        {
           
            student = JsonSerializer.Deserialize<User>(App.Current.Properties["CurrentUser"] as string);
            List<ReservationTile> studentReservation = await WebAPIServices.getStudentReservations(student.UFID);
            if (studentReservation != null)
            {
                studentReserve = new ObservableCollection<ReservationTile> (studentReservation);
            }
            else
            {
                studentReserve = null;
            }

            //properties changed
            onPropertyChanged(nameof(studentReserve));
          onPropertyChanged(nameof(isStudentsVisible));
        });


        public ObservableCollection<ReservationTile> studentReserve { get; set; } = new ObservableCollection<ReservationTile>();
        
       // public ObservableCollection<ReservationTile> studentSessions { get; set; } = new ObservableCollection<ReservationTile>();

        //public bool isStudentsVisible { get { return (studentSessions.Count > 0); } }

        public bool isStudentsVisible
        {
            get
            {
                if (studentReserve == null)
                {
                    return false;
                }
                else
                {
                    return studentReserve.Count > 0;
                }
            }
        }

        public ICommand studentReservationCommand => new Command<object>((selectedItem) =>
        {
            int selectedItemIndex = studentReserve.IndexOf((ReservationTile)selectedItem);

            bool showSubmit = false;
            //if the reservation is not cancelled, not complete, and the end time has passed, allow the showing of the submission prompt
            if (!studentReserve[selectedItemIndex].isCanceled && !studentReserve[selectedItemIndex].isCompleted && studentReserve[selectedItemIndex].toDate < DateTime.Now)
                showSubmit = true;

            ReservationDetails reservationDetails = new ReservationDetails(true, showSubmit, studentReserve[selectedItemIndex].statusMessage);
            reservationDetails.BindingContext = studentReserve[selectedItemIndex];
            reservationDetails.submitRatingCommand = submitRatingCommand;
            Navigation.PushAsync(reservationDetails);

        });

        //command to submit a user rating 
        public ICommand submitRatingCommand => new Command(async (object selectedReservation) =>
        {
            try
            {
                int indexOfReservation = studentReserve.IndexOf((ReservationTile)selectedReservation);


                //TODO ADD API CALL TO SUBMIT RATING
                UserDialogs.Instance.ShowLoading("Submitting Review");
                bool didComplete = await WebAPIServices.submitTutorRating(studentReserve[indexOfReservation]);            
                UserDialogs.Instance.HideLoading();

                if (!didComplete)
                {
                    UserDialogs.Instance.Alert("Save Failed. Please Try Again", null, null);
                    return;
                }


                UserDialogs.Instance.Alert("Review Saved Successfully!", null, null);
                //update reservation on users end immediately
                studentReserve[indexOfReservation].isCompleted = true;
                onPropertyChanged(nameof(studentReserve));
                await Navigation.PopAsync();
            }
            catch (Exception e)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Error submitting rating", null, null);
                Console.WriteLine(e.Message);
            }
        });


    }
}
