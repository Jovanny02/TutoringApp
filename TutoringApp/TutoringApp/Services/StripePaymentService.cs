using System;
using Stripe;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TutoringApp.Models;

namespace TutoringApp.Services
{
    public static class StripePaymentService
    {
        private const string TestApiKey = "pk_test_51HgDLdIgT5WnyGxYgWQTKKY0hVituvtE7bDlwSfrTUeOfftQFFSFDKO2IwVtshXymZ45ZVSlZK3TeLmj3mPOpAn6007zZGiBCw";
        private const string privateKey = "sk_test_51HgDLdIgT5WnyGxYtR5CYUXlWdEuPQKip4H5AwD1kuXhH4e0Y47NRC2kDqE8cE3R2fDWWtf7I94gLOJl0PNrHHcb00aRjIX460";
        public async static Task<bool> PaymentAsync(PaymentInformation paymentInfo)
        {
            bool isTransactionSucess = false;

            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken token = tokenSource.Token;

            try
            {
                await Task.Run(() =>
                 {
                     var Newtoken = CreateToken(paymentInfo);
                     if (Newtoken != null)
                     {
                         isTransactionSucess = MakePayment(Newtoken, paymentInfo);
                         if (isTransactionSucess == true)
                         {
                             CreateTransfer(paymentInfo);
                         }
                     }
                     else
                     {
                         Console.Write("bad credit information");
                     }

                 });

                return isTransactionSucess;

            }

            catch (Exception ex)
            {
                Console.Write("someting wrong");
                throw ex;
            }

        }

        public static bool MakePayment(string tokenID, PaymentInformation paymentInfo)
        {
            try
            {
                StripeConfiguration.SetApiKey(privateKey);
                var options = new ChargeCreateOptions
                {
                    Amount = (long)(paymentInfo.paymentAmount * 100), //change dollar amount into cents
                    Currency = "USD",
                    Description = paymentInfo.Description,
                    Capture = true,
                    Source = tokenID
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

        public static string CreateToken(PaymentInformation paymentInfo)
        {
            try
            {
                StripeConfiguration.SetApiKey(TestApiKey);
                var service = new ChargeService();
                var tokenoptions = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = paymentInfo.CardNumber,
                        Cvc = paymentInfo.SecurityCode,
                        ExpMonth = paymentInfo.ExpMonth,
                        ExpYear = paymentInfo.ExpYear
                    }
                };
                TokenService TokenService = new TokenService();
                Token stripeToken = TokenService.Create(tokenoptions);
                return stripeToken.Id;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public static bool CreateTransfer(PaymentInformation paymentInfo)
        {
            try
            {
                StripeConfiguration.SetApiKey(privateKey);
                var options = new TransferCreateOptions
                {
                    Amount = (long)(paymentInfo.paymentAmount * 100),
                    Currency = "USD",
                    Description="transfer sucess",
                    Destination = "acct_1HvQREEanA2W90kZ"
                };
                var service = new TransferService();
                Transfer TransferId = service.Create(options);
                return true;

            }

           catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
      }


      /*  public static bool ReceiveTransfer(string TransferID)

        {
            try
            {
                var service = new TransferService();
                service.Get(TransferID);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
      */





     }


 }

