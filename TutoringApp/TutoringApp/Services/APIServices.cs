using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TutoringApp.Models;

namespace TutoringApp.Services
{
    public class APIServices
    {
        public async Task<bool> LogininSucess(string Username, string Password, string Email)
        {
            /* var client = new HttpClient();
             var model = new User
             {
                 email = Email,
                 Username = Username,
                 Password = Password
             };
             var json = JsonConvert.SerializeObject(model);
             HttpContent content = new StringContent(json);
             content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
             var response = await client.PostAsync("https://appwebapi20200912230223.azurewebsites.net", content);
             return response.IsSuccessStatusCode; */
            return false;
        }
    }
}
    

