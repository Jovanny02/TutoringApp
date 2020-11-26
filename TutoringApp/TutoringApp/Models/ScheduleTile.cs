using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TutoringApp.Models
{
    public class ScheduleTile : INotifyPropertyChanged
    {

        public ScheduleTile()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DayOfWeek day { get; set; }
        //use number of ticks for assignment of time since timespan cannot be deserialized due to no set methods on properties

        private long StartTicks { get; set; }
        public long EndTicks { get; set; }

        public long startTicks { get { return StartTicks; } 
            set { StartTime = TimeSpan.FromTicks(value); StartTicks = value; } }
        public long endTicks
        {
            get { return EndTicks; }
            set { EndTime = TimeSpan.FromTicks(value); EndTicks = value; }
        }

        [JsonIgnore]
        public string startTimeString
        {
            get
            {
                string returnString = (StartTime.Hours % 12).ToString();
                if (StartTime.Hours == 0)
                    returnString = "12 am";
                else if (StartTime.Hours == 12)
                    returnString = "12 pm";
                else if (StartTime.Hours < 12)
                    returnString += " am";
                else
                    returnString += " pm";

                return returnString;
            }
        }

        [JsonIgnore]
        public string endTimeString
        {
            get
            {
                string returnString = (EndTime.Hours % 12).ToString();
                if (EndTime.Hours == 0)
                    returnString = "12 am";
                else if (EndTime.Hours == 12)
                    returnString = "12 pm";
                else if (EndTime.Hours < 12)
                    returnString += " am";
                else
                    returnString += " pm";

                return returnString;
            }
        }


        //ignore these properties so they do not overwrite start and end ticks on deserialization 
        [JsonIgnore]
        private TimeSpan StartTime { get; set; }
        [JsonIgnore]
        public TimeSpan startTime { get { return StartTime; } 
            set {
                StartTime = value.Add(new TimeSpan(0, -1* value.Minutes, 0));
                startTicks = StartTime.Ticks;
                onPropertyChanged();
            }
        }
        [JsonIgnore]
        private TimeSpan EndTime { get; set; }
        [JsonIgnore]
        public TimeSpan endTime
        {
            get { return EndTime; }
            set
            {
                EndTime = value.Add(new TimeSpan(0, -1 * value.Minutes, 0));
                EndTicks = EndTime.Ticks;
                onPropertyChanged();
            }
        }
        private bool isUnavailable { get; set; } = false;
        public bool IsUnavailable
        {
            get { return isUnavailable; }
            set
            {
                isUnavailable = value;
                IsEnabled = !isUnavailable;
            }
        }
        private bool isEnabled { get; set; } = true;
        public bool IsEnabled { 
            get { return isEnabled; } 
            set { 
                isEnabled = value; 
                onPropertyChanged(); 
            } 
        }

        public string dayString { get { return day.ToString(); } }
    }
}
