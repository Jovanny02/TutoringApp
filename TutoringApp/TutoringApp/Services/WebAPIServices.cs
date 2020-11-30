using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using TutoringApp.Models;
using System.Net.Http.Headers;

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




    }
}
