using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TutoringApp.Models
{
    public class ReservationTile : Reservation
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

        public bool isCompleted { get { return IsCompleted; } set { IsCompleted = value; onPropertyChanged(); onPropertyChanged(nameof(statusMessage));  } } 

        [JsonIgnore]
        public string formattedFromDate { get { return (fromDate.ToString("MMM d: ") + fromDateString); } }

        [JsonIgnore]
        public string statusMessage
        {
            get
            {
                if (isCompleted)
                {
                    return "Completed";
                }
                else if (!isCompleted && toDate < DateTime.Now)
                {
                    return "Awaiting Review";
                }
                else if (!isCompleted && fromDate < DateTime.Now && DateTime.Now < toDate)
                {
                    return "In Progress";
                }
                else if (!isCompleted && DateTime.Now < fromDate)
                {
                    return "Upcoming";

                }
                else
                {
                    return string.Empty;
                }
            }
        }

    }
}
