using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    public class TutorInfo : User
    {
        public string ratingLabel { get { return string.Format("{0:0.0}", Math.Truncate(AverageRating * 10) / 10); } }

        public string RequestedPay
        {
            get { return "$" + requestedPay.ToString(); }
        }
    }
}
