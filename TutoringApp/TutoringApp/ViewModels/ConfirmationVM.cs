using System;
using System.Collections.Generic;
using System.Text;
using TutoringApp.Services;
using TutoringApp.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using TutoringApp.Views;
using System.Text.Json;
using System.Threading.Tasks;

namespace TutoringApp.ViewModels
{
    public class ConfirmationVM : BaseVM
    {
        public ConfirmationVM(object tutor, object reservations)
        {
            this.tutor = (TutorInfo)tutor;         

            this.reservations = (List<Reservation>)reservations;

            totalCost = this.tutor.requestedPay * this.reservations.Count;
            onPropertyChanged(nameof(totalCost));
        }

        public List<Reservation> reservations { get; set; } = new List<Reservation>();
        public TutorInfo tutor { get; set; } = new TutorInfo();

        public PaymentInformation payInfo { get; set; } = new PaymentInformation();

        public int totalCost { get; set; }

        public string totalCostString { get { return "$" + totalCost.ToString(); } }

        public DateTime selectedDate { get { return reservations[0].fromDate; } }

       public ICommand tapCardCommand => new Command(() => {
           Payment cardPage = new Payment();
           cardPage.BindingContext = new PaymentInformation(); ;
           cardPage.saveCommand = saveCardCommand;
           Navigation.PushAsync(cardPage);
           
       });

        public ICommand saveCardCommand => new Command((object tempInformation) => {

            PaymentInformation tempPayInfo = (PaymentInformation)tempInformation;
            if(tempPayInfo.CardNumber.Length < 13 || tempPayInfo.CardNumber.Contains(".") )
            {
                UserDialogs.Instance.Alert("Incorrect card number", null, "OK");
                return;
            }
            else if(tempPayInfo.SecurityCode.Length != 3 || tempPayInfo.SecurityCode.Contains("."))
            {
                UserDialogs.Instance.Alert("Incorrect security code", null, "OK");
                return;
            }
            else if (tempPayInfo.ExpiryDate.Length != 5 || tempPayInfo.ExpiryDate.Contains("."))
            {
                UserDialogs.Instance.Alert("Incorrect expiration month or year", null, "OK");
                return;
            }
            else if (tempPayInfo.CardName.Length == 0)
            {
                UserDialogs.Instance.Alert("Name cannot be empty", null, "OK");
                return;
            }
            payInfo = tempPayInfo;
            onPropertyChanged(nameof(payInfo));
            Navigation.PopAsync();
        });

        public ICommand confirmCommand => new Command(async () => {
            
            if(payInfo.CardNumber == String.Empty)
            {
                UserDialogs.Instance.Alert("Please fill card information", null, "OK");
                return;
            }

            if (!App.Current.Properties.ContainsKey("CurrentUser"))
            {
                UserDialogs.Instance.Alert("Error, you are not logged in", null, "OK");
                return;
            }
            User currUser = JsonSerializer.Deserialize<User>(App.Current.Properties["CurrentUser"] as string);

            payInfo.Description = "Tutor: " + tutor.name + "(" + tutor.UFID + ") " + "Student: " + currUser.name + "(" + currUser.UFID + ") Date: " + selectedDate.ToShortDateString();
            payInfo.paymentAmount = totalCost;


            try
            {            
                //Process payment
                UserDialogs.Instance.ShowLoading("Processing Payment");
                await StripePaymentService.PaymentAsync(payInfo, tutor.stripeAccountID);
                UserDialogs.Instance.HideLoading();


                //Make API call to store reservation into database
                UserDialogs.Instance.ShowLoading("Saving Reservation");
                string reservationResult = await WebAPIServices.setReservations(reservations);
                UserDialogs.Instance.HideLoading();

                if (reservationResult != "success")
                {
                    UserDialogs.Instance.Alert(reservationResult, null, null);
                    return;
                }



            }
            catch (Exception e)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert(e.Message, null, "OK");
                return;
            }
            //alert that it was saved successfully and to return to home page
            UserDialogs.Instance.Alert("Reservation Saved Successfully", null, null);
            await Navigation.PopToRootAsync();
        });



    }
}
