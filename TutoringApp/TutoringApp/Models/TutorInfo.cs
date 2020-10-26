using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    class TutorInfo : User
    {

        public int displayPosition { get; set; }
        public string ratingLabel { get { return string.Format("{0:0.0}", Math.Truncate(AverageRating * 10) / 10); } }

        public string RequestedPay
        {
            get { return "$" + requestedPay.ToString(); }
        }
    }
}
