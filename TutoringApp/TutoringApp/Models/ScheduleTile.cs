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
        public long startTicks {get; set; }
        public long endTicks {get; set; }
        //ignore these properties so they do not overwrite start and end ticks on deserialization 
        
        [JsonIgnore]
        private TimeSpan StartTime { get; set; }
        [JsonIgnore]
        public TimeSpan startTime { get { return StartTime; } 
            set {
                StartTime = value; 
                startTicks = StartTime.Ticks; 
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
                EndTime = value; 
                endTicks = EndTime.Ticks;
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
