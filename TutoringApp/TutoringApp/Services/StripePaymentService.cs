﻿using System;
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
                        // if (isTransactionSucess == true)
                         //{
                         //    CreateTransfer(paymentInfo, stripeAccountID);
                        // }
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

        public static bool CreateTransfer(double paymentAmount, string stripeAccountID)
        {
            //Convert to cents, find percentage after percent fee and standard 30 cent fee
            paymentAmount = ((paymentAmount * 100) * .971) - 30; //payout amount AFTER stripe fee

            try
            {
                StripeConfiguration.ApiKey = privateKey;
                var options = new TransferCreateOptions
                {
                    Amount = (long)(paymentAmount),
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

        public static string makeAccount(User currUser, stripeAccountInfo accountInfo)
        {
            StripeConfiguration.ApiKey = privateKey;
            int nameIndex = currUser.name.IndexOf(' ');
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
                        Cvc = accountInfo.cardInfo.SecurityCode,
                        Currency = "usd",
                        Name = accountInfo.cardInfo.CardName,
                        Number = accountInfo.cardInfo.CardNumber,
                        ExpMonth = accountInfo.cardInfo.ExpMonth,
                        ExpYear = accountInfo.cardInfo.ExpYear
                    },
                    BusinessType = "individual",
                    Individual = new AccountIndividualOptions
                    {
                        FirstName = currUser.name.Substring(0, nameIndex),
                        LastName = currUser.name.Substring(nameIndex + 1, (currUser.name.Length - (nameIndex + 1))),
                        Dob = accountInfo.dob,
                        SsnLast4 = accountInfo.lastFourSSN,
                        Phone = accountInfo.phoneNumber,
                        Address = new AddressOptions { City = accountInfo.address.City, 
                            Country = "US", Line1 = accountInfo.address.Line1, PostalCode = accountInfo.address.PostalCode, State = accountInfo.address.State},
                        Email = currUser.email
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

