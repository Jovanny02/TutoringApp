using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using TutoringApp.Models;

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

        public static async void signUpUser(User newUser)
        {
            //Use this refernce to make the post request using httpclient https://stackoverflow.com/questions/36625881/how-do-i-pass-an-object-to-httpclient-postasync-and-serialize-as-a-json-body
            //or this reference https://stackoverflow.com/questions/19610883/sending-c-sharp-object-to-webapi-controller
        }



    }
}
