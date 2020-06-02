using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    public class EducationTile
    {
        public string Major { get; set; }

        public string fromDate { get; set; }

        public string toDate { get; set; }

        public string University { get; set; }

        public string location { get; set; }

        public string iconSrc { get; set; }

        public string degreeDescription { get; set; }
        public string fullDate
        {
            get
            {
                return string.Format("{0} - {1}", fromDate, toDate);
            }
        }
    }
}
