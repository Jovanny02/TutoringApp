using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TutoringApp.Models
{
    public class Reservation : INotifyPropertyChanged
    {

        [JsonIgnore]
        private bool IsSelected { get; set; } = false;

        [JsonIgnore]
        public bool isSelected { get { return IsSelected; } set { IsSelected = value; onPropertyChanged(); } } 
        public bool isCanceled { get; set; } = false;

        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public int studentUFID { get; set; }
        public int tutorUFID { get; set; }

        [JsonIgnore]
        public string fromDateString
        {
            get
            {
                string returnString = (fromDate.Hour % 12).ToString();
                if (fromDate.Hour == 0)
                    returnString = "12 am";
                else if (fromDate.Hour == 12)
                    returnString = "12 pm";
                else if (fromDate.Hour < 12)
                    returnString += " am";
                else
                    returnString += " pm";

                return returnString;
            }
        }

        [JsonIgnore]
        public string toDateString
        {
            get
            {
                string returnString = (toDate.Hour % 12).ToString();
                if (toDate.Hour == 0)               
                    returnString = "12 am";                
                else if (toDate.Hour == 12)               
                    returnString = "12 pm";              
                else if(toDate.Hour < 12)
                    returnString += " am";
                else
                    returnString += " pm";

                return returnString;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
