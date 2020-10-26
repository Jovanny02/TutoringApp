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
            SaveCommand = new Command(() =>
            {
                
                //TODO create sing up call using information in password, user, and email
                Console.WriteLine("Triggered Sign Up Command");
                Navigation.PopAsync();
            });
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
                UserDialogs.Instance.ShowLoading("Payement Processing ...");
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


        public ICommand SaveCommand { protected set; get; }
        public string CardName { get; set; }
     
        public string SecurityCode { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string ExpiryDate { get; set; }
    }
}
