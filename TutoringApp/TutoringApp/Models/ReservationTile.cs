using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TutoringApp.Models
{
    public class ReservationTile : Reservation, INotifyPropertyChanged
    {
        public string tutorName { get; set; }
        public string studentName { get; set;}
        public string tutorPicture{ get; set; } = "user.png";
        public string studentPicture { get; set; } = "user.png";
        public string zoomLink { get; set; }

        private double Rating { get; set; } = 0.0;
        [JsonIgnore]
        public double rating { get { return Rating; }
            set { 
                Rating = value; 
                onPropertyChanged(); } }

        private bool IsCompleted { get; set; } = false;
        [JsonIgnore]

        public bool isCompleted { get { return IsCompleted; } set { IsCompleted = value; onPropertyChanged(); } } 

        [JsonIgnore]
        public string formattedFromDate { get { return (fromDate.ToString("MMM d - ") + fromDateString); } }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
