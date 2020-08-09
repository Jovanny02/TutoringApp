using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    class EducationSection
    {
        public string Major { get; set; }

        public string fromYear { get; set; }

        public string toYear { get; set; }

        public string University { get; set; }
        public string fullDate
        {
            get
            {
                return string.Format("{0} - {1}", fromYear, toYear);
            }
        }

        public int key = int.MaxValue;
    }
}
