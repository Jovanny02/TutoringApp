using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace TutoringApp.Models
{
    public class PaymentInformation : INotifyPropertyChanged
    {
        public string CardName { get; set; } = "";
        public string CardNumber { get; set; } = "";
        public string Description { get; set; } = "";
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        private string expiryDate { get; set; } = "";
        public string ExpiryDate
        {
            get { return expiryDate; }
            set
            {
                //check length to see if it is a full month and year
                if (value.Length != 5)
                {
                    //logic for adding forward slash
                    if (expiryDate.Length == 1 && value.Length == 2)
                    {
                        expiryDate = value + "/";
                    }
                    //logic for removing forward slash
                    else if (expiryDate.Length == 3 && value.Length == 2)
                    {
                        expiryDate = value.Substring(0, 1);
                    }
                    else
                    {
                        expiryDate = value;
                    }

                    ExpMonth = 0;
                    ExpYear = 0;
                }
                else
                {
                    //parse month
                    int tempInt = 0;
                    Int32.TryParse(value.Substring(0, 2), out tempInt);
                    if (tempInt > 12)
                        ExpMonth = 12;
                    else if (tempInt < 1)
                        ExpMonth = 1;
                    else
                        ExpMonth = tempInt;


                    //parse year
                    int tempInt2 = 0;
                    Int32.TryParse("20" + value.Substring(3, 2), out tempInt2);
                    if (tempInt2 < DateTime.Now.Year)
                        ExpYear = DateTime.Now.Year;
                    else if (tempInt2 > DateTime.Now.Year + 30)
                        ExpYear = DateTime.Now.Year + 30;
                    else
                        ExpYear = tempInt2;
                    //set string equal to corrected values and account for leading zero
                    string tempString = ExpMonth.ToString() + "/" + ExpYear.ToString().Substring(2, 2);
                    if (tempString.Length == 4)
                        expiryDate = "0" + tempString;
                    else
                        expiryDate = tempString;

                }
                onPropertyChanged();
            }
        }
        public string SecurityCode { get; set; } = "";
        public int paymentAmount { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
