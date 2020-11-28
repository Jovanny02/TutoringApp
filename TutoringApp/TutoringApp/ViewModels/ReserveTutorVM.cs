﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TutoringApp.Models;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using TutoringApp.Views;

namespace TutoringApp.ViewModels
{
    public class ReserveTutorVM : BaseVM
    {
        public ReserveTutorVM(object tutor)
        {
            this.tutor = (TutorInfo)tutor;
            minDate = DateTime.Today.AddDays(-1 * DateTime.Today.Day + 1);
            currentDay = DateTime.Now.AddDays(-1);
            lastDay = DateTime.Now.AddDays(21);
            MonthYear = DateTime.Now;
            listText = "Select a day to see available sessions";
            isListVisible = false;
        }
        public TutorInfo tutor { get; set; }

        public DateTime MonthYear { get; set; }
        public DateTime currentDay { get; set; }
        public DateTime lastDay { get; set; }
        public DateTime minDate { get; set; }

        public int currentMonth { get { return currentDay.Month; }  }
        public int currentYear { get { return currentDay.Year; } }

        public ObservableCollection<Reservation> reservationList { get; set; }

        private string ListText { get; set; }
        public string listText { get { return ListText; } set { ListText = value; onPropertyChanged(); } }
        private bool IsListVisible { get; set; }

        public bool isListVisible
        {
            get { return IsListVisible; }
            set { IsListVisible = value; onPropertyChanged(); }
        }

        public void handleTappedReservation(object selectedReservation)
        {
            Reservation reservation = (Reservation)selectedReservation;

            int index = reservationList.IndexOf(reservation);

            if (index == -1) //return due to error
            {
                return;
            }
            else
            {
                reservationList[index].isSelected = !reservationList[index].isSelected;
            }

           // onPropertyChanged(nameof(reservationList));
        }



        public ICommand DayTappedCommand => new Command<DateTime>((selectedDate) => {
            //find index that holds info for selected day day      
            int index;
            for(index = 0; index < tutor.ScheduleSections.Count; index++)
            {
                if (tutor.ScheduleSections[index].day == selectedDate.DayOfWeek)
                    break;
            }
            //if index equals the count, some error occured so just return
            if (index == tutor.ScheduleSections.Count)
                return;

            //clear list
            reservationList = new ObservableCollection<Reservation>();
            if(selectedDate < DateTime.Today)
            {
                isListVisible = false;
                listText = "No sessions available";
                onPropertyChanged(nameof(reservationList));
                return;
            }


            int startingHour = tutor.ScheduleSections[index].startTime.Hours;
            int endingHour = tutor.ScheduleSections[index].endTime.Hours;
            //assign the set of schedule for each day
            while((startingHour < endingHour) && !tutor.ScheduleSections[index].IsUnavailable)
            {
                //set dateTime as selected date plus the hours
                DateTime tempStartDateTime = selectedDate;
                tempStartDateTime = tempStartDateTime.AddHours(startingHour);
                DateTime tempEndDateTime = selectedDate;
                tempEndDateTime = tempEndDateTime.AddHours(startingHour + 1);


                reservationList.Add(new Reservation {
                isSelected = false,
                isCanceled = false,
                fromDate = tempStartDateTime,
                toDate = tempEndDateTime,
                tutorUFID = tutor.UFID,
                studentUFID = 12345678    //need to add check to get current user UFID
                });

                startingHour++;
            }


            if(reservationList.Count == 0)
            {
                isListVisible = false;
                listText = "No sessions available";
            }
            else
            {
                isListVisible = true;
            }

            onPropertyChanged(nameof(reservationList));
        });


        public ICommand SwipeLeftCommand => new Command(() => { MonthYear = MonthYear.AddMonths(1); });
        public ICommand SwipeRightCommand => new Command(() => { MonthYear = MonthYear.AddMonths(-1); });

        public ICommand confirmationCommand => new Command(() => {
            List<Reservation> selectedReservations = new List<Reservation>();
            for (int i = 0; reservationList != null && i < reservationList.Count ; i++)
            {
                if (reservationList[i].isSelected)
                    selectedReservations.Add(reservationList[i]);
            }

            if(selectedReservations.Count == 0)
            {
                UserDialogs.Instance.Alert("You must select at least 1 section to continue", null, null);
                return;
            }
            Navigation.PushAsync(new Confirmation(tutor, selectedReservations));

        });

    }
}
