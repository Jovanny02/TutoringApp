using System;
using System.Collections.Generic;
using System.Text;
using Stripe;

namespace TutoringApp.Models
{
    public class stripeAccountInfo
    {

        public PaymentInformation cardInfo { get; set; } = new PaymentInformation();

        public AddressOptions address { get; set; } = new AddressOptions();

        public string phoneNumber { get; set; } = "";
        public string lastFourSSN { get; set; } = "";

        public DobOptions dob { get; set; } = new DobOptions();
        private DateTime DobDateTime { get; set; } = DateTime.Now;

        public DateTime dobDateTime { get { return DobDateTime; }
            set {
                DobDateTime = value;
                dob.Day = value.Day;
                dob.Month = value.Month;
                dob.Year = value.Year;
            } 
        }



    }
}
