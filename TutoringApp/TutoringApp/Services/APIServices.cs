using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TutoringApp.Models;
using TutoringApp.RestApiClass;


namespace TutoringApp.Services
{
    public class APIServices
    {
        //public async Task<bool> LogininSucess(string Username, string Password)
        // {
        //  var client = new HttpClient();
        //var model = new User


        //  name = Username,
        // password = Password
        //};
        //var json = JsonConvert.SerializeObject(model);
        //HttpContent content = new StringContent(json);
        //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //var response = await client.PostAsync("https://appwebapi20200912230223.azurewebsites.net/api/LoginController/Signup", content);
        //return response.IsSuccessStatusCode;
        //}


        RestClient<User> _restClient = new RestClient<User>();
        public async Task<bool> CheckLoginIfExists(string username, string password)
        {
            var check = await _restClient.checkLogin(username, password);
            return check;

        }


        public async Task<bool> CheckSignupIfExists(string name, string password,string email,int ufid )
        {
            var check = await _restClient.checkSignup(name, password,email,ufid);
            return check;

        }

       
    }
}
    

