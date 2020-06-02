using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    public class WorkTile
    {
        public string jobTitle { get; set; }

        public string fromDate { get; set; }

        public string toDate { get; set; }

        public string companyName { get; set; }

        public string location { get; set; }

        public string iconSrc { get; set; }

        public string jobDescription { get; set; }
        public string fullDate 
        { 
            get {
                return string.Format("{0} - {1}", fromDate, toDate);
            } 
        }
    }
}
