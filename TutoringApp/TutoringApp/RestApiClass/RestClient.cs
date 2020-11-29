using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TutoringApp.Models;

namespace TutoringApp.RestApiClass
{
    class RestClient<T>
    {
       
    private const string MainWebServiceUrl = "https://appwebapi20200912230223.azurewebsites.net/api/Login?username=";
        private const string GetwebURL = "https://appwebapi20200912230223.azurewebsites.net/api/Values";
        



        public async Task<bool> checkLogin(string username, string password)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(MainWebServiceUrl +username + "&password="+ password);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> checkSignup(string username, string password)
        {
            
            
             var client = new HttpClient();
            var model = new User { 

               name = username,
             password = password
            };

            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(MainWebServiceUrl + username + "&password=" + password, content);
            return response.IsSuccessStatusCode;
            
        }
        public async Task<User> GetAsync(string id)
        {
            var client = new HttpClient();
            User value = null;
           
            HttpResponseMessage response = await client.GetAsync(GetwebURL+"/{"+id+"}");
            if (response.IsSuccessStatusCode)
            {
                value = await response.Content.ReadAsAsync<User>();
            }
            return value;
        }

    }
}
