using Stripe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TutoringApp.Views;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace TutoringApp.ViewModels
{
    class PaymentVM : BaseVM
    {
        public PaymentVM()
        {
            //give expiration year a default value
        }
        public Token stripeToken;
        public TokenService TokenService;
        public string TestApiKey = "pk_test_51HgDLdIgT5WnyGxYgWQTKKY0hVituvtE7bDlwSfrTUeOfftQFFSFDKO2IwVtshXymZ45ZVSlZK3TeLmj3mPOpAn6007zZGiBCw";
       
        public string CardNumber { get;  set; }


        public async Task PaymentAsync()
        {
            bool isTransactionSucess = false;

            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            try
            {
                UserDialogs.Instance.ShowLoading("Payment Processing ...");
                await Task.Run(async () =>
               {
                   var Newtoken = CreateToken();
                   if (Newtoken != null)
                   {
                       isTransactionSucess = MakePayment();
                   }
                   else
                   {
                       UserDialogs.Instance.Alert("Bad credit card information",null,"OK");

                       Console.Write("bad credit information");
                   }

               });
            }

            catch(Exception ex)
            {
                Console.Write("someting wrong");
                throw ex;
            }

            finally
            {
                if (isTransactionSucess)
                {
                    UserDialogs.Instance.Alert("Sucess", "Sucess", "OK");
                    UserDialogs.Instance.HideLoading();
                }
                else
                {
                    
                    UserDialogs.Instance.HideLoading();
                    UserDialogs.Instance.Alert("something went wrong", "Payment failed", "Ok");

                }
            }


          



        }
        public bool MakePayment()
        {
            try
            {
                StripeConfiguration.SetApiKey("sk_test_51HgDLdIgT5WnyGxYtR5CYUXlWdEuPQKip4H5AwD1kuXhH4e0Y47NRC2kDqE8cE3R2fDWWtf7I94gLOJl0PNrHHcb00aRjIX460");
                var options = new ChargeCreateOptions
                {
                    Amount = (long)float.Parse("1000"),
                    Currency = "USD",
                    Description = "charge test 1",
                    Capture = true,
                    Source = stripeToken.Id



                };
                var service = new ChargeService();
                Charge charge = service.Create(options);
                return true;
            
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public string CreateToken()
        {
            try
            {
                StripeConfiguration.SetApiKey(TestApiKey);
                var service = new ChargeService();
                var tokenoptions = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = CardNumber,
                        Cvc = SecurityCode,
                        ExpMonth = ExpMonth,
                        ExpYear = ExpYear



                    }
                };
                TokenService = new TokenService();
                stripeToken = TokenService.Create(tokenoptions);
                return stripeToken.Id;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        } 


        public string CardName { get; set; }
     
        public string SecurityCode { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        private string expiryDate { get; set; } = "";

        public string ExpiryDate
        {
            get { return expiryDate; }
            set {
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
                    else if(tempInt2 > DateTime.Now.Year + 30)
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

    }
}
