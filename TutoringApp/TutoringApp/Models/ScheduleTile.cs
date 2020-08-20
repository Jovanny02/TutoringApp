using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    class ScheduleTile
    {
        public DayOfWeek day;

        public TimeSpan startTime = new TimeSpan();

        public TimeSpan endTime = new TimeSpan();

        public bool isUnavailable { get; set; } = false;

        public bool isEnabled { get { return !isUnavailable; } }

        public string dayString { get { return day.ToString(); } }
    }
}
