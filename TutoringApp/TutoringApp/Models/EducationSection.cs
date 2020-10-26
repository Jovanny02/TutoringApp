using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    public class EducationSection
    {
        public EducationSection()
        {
            key = int.MaxValue;
        }


        public string Major { get; set; }

        public int fromYear { get; set; } = DateTime.Now.Year;

        public int toYear { get; set; } = DateTime.Now.Year;

        public string University { get; set; }
        public string fullDate
        {
            get
            {
                return string.Format("{0} - {1}", fromYear, toYear);
            }
        }

        public int key { get; set; }
    }
}
