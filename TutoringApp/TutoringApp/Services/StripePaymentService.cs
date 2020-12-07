using System;
using Stripe;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TutoringApp.Models;
using System.Net;

namespace TutoringApp.Services
{
    public static class StripePaymentService
    {
        private const string TestApiKey = "pk_test_51HgDLdIgT5WnyGxYgWQTKKY0hVituvtE7bDlwSfrTUeOfftQFFSFDKO2IwVtshXymZ45ZVSlZK3TeLmj3mPOpAn6007zZGiBCw";
        private const string privateKey = "sk_test_51HgDLdIgT5WnyGxYtR5CYUXlWdEuPQKip4H5AwD1kuXhH4e0Y47NRC2kDqE8cE3R2fDWWtf7I94gLOJl0PNrHHcb00aRjIX460";
        public async static Task<bool> PaymentAsync(PaymentInformation paymentInfo, string stripeAccountID)
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
                             CreateTransfer(paymentInfo, stripeAccountID);
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
                StripeConfiguration.ApiKey = privateKey;
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
                StripeConfiguration.ApiKey = TestApiKey;
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

        public static bool CreateTransfer(PaymentInformation paymentInfo, string stripeAccountID)
        {
            try
            {
                StripeConfiguration.ApiKey = privateKey;
                var options = new TransferCreateOptions
                {
                    Amount = (long)(paymentInfo.paymentAmount * 100),
                    Currency = "USD",
                    Description="transfer sucess",
                    Destination = stripeAccountID
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

        public static string makeAccount()
        {
            StripeConfiguration.ApiKey = privateKey;

            try
            {
                var options = new AccountCreateOptions
                {
                    Type = "custom",
                    Country = "US",
                    Capabilities = new AccountCapabilitiesOptions
                    {
                        CardPayments = new AccountCapabilitiesCardPaymentsOptions
                        {
                            Requested = true,
                        },
                        Transfers = new AccountCapabilitiesTransfersOptions
                        {
                            Requested = true,
                        },
                    },
                    ExternalAccount = new AccountCardOptions
                    {
                        Cvc = "123",
                        Currency = "usd",
                        Name = "Kareem Test",
                        Number = "4000056655665556",
                        ExpMonth = 12,
                        ExpYear = 2021
                    },
                    BusinessType = "individual",
                    Individual = new AccountIndividualOptions
                    {
                        FirstName = "Fuck",
                        LastName = "This",
                        Dob = new DobOptions {Day = 1, Month = 1, Year=1901 },
                        SsnLast4 = "0000",
                        Phone = "0000000000",
                        Address = new AddressOptions { City = "gainesville", Country = "US", Line1 = "4000 sw 37 blvd", PostalCode = "32608", State = "Florida" },
                        Email = "testemail@gmail.com",
                    },
                    TosAcceptance = new AccountTosAcceptanceOptions
                    {
                        Ip = getMyIP(),
                        Date = DateTime.Now,

                    },
                    BusinessProfile = new AccountBusinessProfileOptions
                    {
                        Mcc = "8299", //education services
                        Url = "https://www.ufl.edu/",
                        SupportUrl = "https://www.ufl.edu/"
                    }

                };

                var service = new AccountService();
                var account = service.Create(options);


                return account.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return String.Empty;
                throw ex;
            }
        }

        private static string getMyIP()
        {
            string MyIp;
            foreach (IPAddress adress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                MyIp = adress.ToString();
                return MyIp;
            }
            Exception e = new Exception();
            throw e;
        }


    }


 }

