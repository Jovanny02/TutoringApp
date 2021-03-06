﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using TutoringApp.Models;
using System.Net.Http.Headers;
using System.Net;

namespace TutoringApp.Services
{
    public static class WebAPIServices
    {
        private static string debugURLString = "http://10.0.2.2:49836/api/";
        private static string productionURLString = "https://appwebapi20200912230223.azurewebsites.net/api/";

        private static HttpClient httpClient = new HttpClient();

        //http://10.0.2.2:49836/api//Login?UFID=12345678&password=testPass@1
        public static async Task<bool> checkLoginCredentials(string UFID, string password)
        {
            try
            {
                #if (DEBUG)
                string JSONResult = await httpClient.GetStringAsync(debugURLString + "Login?UFID=" + UFID + "&password=" + password);
                #elif(!DEBUG)
                string JSONResult = await httpClient.GetStringAsync(productionURLString + "Login?UFID=" + UFID + "&password=" + password);

                #endif

                if (JSONResult == "Please Enter valid UserName and Password")
                {
                    return false;
                }

                App.Current.Properties.Add("CurrentUser", JSONResult);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public static async Task<bool> signUpUser(User newUser)
        {
            string userString = JsonSerializer.Serialize(newUser);
            try
            { 

                var stringContent = new StringContent(userString, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

                #if (DEBUG)
                var response = await httpClient.PostAsync(debugURLString + "Login/UserSignUp", stringContent);
                #elif (!DEBUG)
                var response = await httpClient.PostAsync(productionURLString + "Login/UserSignUp", stringContent);
                #endif

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }          
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;

            }

            //add user to dictionary
            App.Current.Properties.Add("CurrentUser", userString);
            return true;

        }

        public static async Task<bool> updateUser(string userString)
        {
            try
            {
                //  var buffer = System.Text.Encoding.UTF8.GetBytes(userString);
                //  var byteContent = new ByteArrayContent(buffer);
                // byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //userString = Uri.EscapeDataString(userString);
                var stringContent = new StringContent(userString, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+
                
                #if (DEBUG)
                var response = await httpClient.PostAsync(debugURLString + "values/updateUser" , stringContent);
                #elif (!DEBUG)
                var response = await httpClient.PostAsync(productionURLString + "values/updateUser" , stringContent);
                #endif

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }          
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;

            }
            return true;
        }

        public static async Task<List<TutorInfo>> getTutors(string course)
        {
            try
            {

                #if (DEBUG)
                string JSONResult = await httpClient.GetStringAsync(debugURLString + "values/getTutors?searchedCourse=" + course);
                #elif (!DEBUG)
                string JSONResult = await httpClient.GetStringAsync(productionURLString + "values/getTutors?searchedCourse=" + course);
                #endif

                List<TutorInfo> tutors = JsonSerializer.Deserialize<List<TutorInfo>>(JSONResult);

                return tutors;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;

            }
        }
        //getTutorReservations?tutorUFID={tutorUFID}
        public static async Task<List<Reservation>> getTutorReservations(int tutorUFID)
        {
            try
            {

                #if (DEBUG)
                string JSONResult = await httpClient.GetStringAsync(debugURLString + "values/getTutorReservations?tutorUFID=" + tutorUFID);
                #elif (!DEBUG)
                string JSONResult = await httpClient.GetStringAsync(productionURLString + "values/getTutorReservations?tutorUFID=" + tutorUFID);
                #endif

                List<Reservation> tutorsReservations = JsonSerializer.Deserialize<List<Reservation>>(JSONResult);

                return tutorsReservations;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;

            }
        }

        public static async Task<List<ReservationTile>> getStudentReservations(int studentUFID)
        {
            try
            {

                #if (DEBUG)
                string JSONResult = await httpClient.GetStringAsync(debugURLString + "values/getStudentReservations?studentUFID=" + studentUFID);
                #elif (!DEBUG)
                string JSONResult = await httpClient.GetStringAsync(productionURLString +"values/getStudentReservations?studentUFID=" + studentUFID );
                #endif

                List<ReservationTile> studentReservations = JsonSerializer.Deserialize<List<ReservationTile>>(JSONResult);

                return studentReservations;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;

            }
        }


        public static async Task<List<ReservationTile>> getTutorReservationTiles(int tutorUFID)
        {
            try
            {
                #if (DEBUG)
                string JSONResult = await httpClient.GetStringAsync(debugURLString + "values/getTutorReservationTiles?tutorUFID=" + tutorUFID);
                #elif (!DEBUG)
                string JSONResult = await httpClient.GetStringAsync(productionURLString +"values/getTutorReservationTiles?tutorUFID=" + tutorUFID );
                #endif

                List<ReservationTile> studentReservations = JsonSerializer.Deserialize<List<ReservationTile>>(JSONResult);

                return studentReservations;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;

            }
        }




        public static async Task<string> setReservations(List<Reservation> reservationList)
        {
            string reservationsString = JsonSerializer.Serialize(reservationList);
            try
            { 
                var stringContent = new StringContent(reservationsString, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

                #if (DEBUG)
                var response = await httpClient.PostAsync(debugURLString + "values/setReservations", stringContent);
                #elif (!DEBUG)
                var response = await httpClient.PostAsync(productionURLString + "values/setReservations", stringContent);
                #endif

                //"Tutor and student are the same"

                if (!response.IsSuccessStatusCode)
                {
                    HttpContent requestContent = response.Content;
                    string jsonContent = await requestContent.ReadAsStringAsync();

                    return jsonContent;
                }
            }          
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "An error occured, please try again";

            }

            //added reservations successfully
            return "success";

        }


        public static async Task<bool> submitTutorRating(ReservationTile reservation)
        {
            string reservationString = JsonSerializer.Serialize(reservation);

            try
            {
                var stringContent = new StringContent(reservationString, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

                #if (DEBUG)
                var response = await httpClient.PutAsync(debugURLString + "values/submitRating", stringContent);
                #elif (!DEBUG)
                var response = await httpClient.PostAsync(productionURLString + "values/submitRating", stringContent);
                #endif


                if (!response.IsSuccessStatusCode)
                {
                    HttpContent requestContent = response.Content;
                    string jsonContent = await requestContent.ReadAsStringAsync();
                    Console.WriteLine(jsonContent);
                    return false;
                }



                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;

            }
        }

        public static async Task<bool> setPaymentReceived(ReservationTile reservation)
        {
            string reservationString = JsonSerializer.Serialize(reservation);

            try
            {
                var stringContent = new StringContent(reservationString, UnicodeEncoding.UTF8, "application/json"); // use MediaTypeNames.Application.Json in Core 3.0+ and Standard 2.1+

                #if (DEBUG)
                var response = await httpClient.PutAsync(debugURLString + "values/setPaymentReceived", stringContent);
                #elif (!DEBUG)
                var response = await httpClient.PostAsync(productionURLString + "values/setPaymentReceived", stringContent);
                #endif


                if (!response.IsSuccessStatusCode)
                {
                    HttpContent requestContent = response.Content;
                    string jsonContent = await requestContent.ReadAsStringAsync();
                    Console.WriteLine(jsonContent);
                    return false;
                }



                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;

            }
        }




    }
}
