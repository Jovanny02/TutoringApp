using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    class ScheduleTile : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DayOfWeek day;

        public TimeSpan startTime = new TimeSpan(0);

        public TimeSpan endTime = new TimeSpan(0);

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
