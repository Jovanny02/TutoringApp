using AppWebAPI.Models;
using AppWebAPI.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AppWebAPI.Controllers
{
    public class LoginController : ApiController

    {



        TutoringAppDBEntities db = new TutoringAppDBEntities();


        [HttpPost]
        [Route("api/Login?username={username}&password={password}")]
        [ActionName("XAMARIN_REG")]
        // POST: api/Login  
        public HttpResponseMessage Xamarin_reg(string username, string password)
        {
            user login = new user();
            login.fullName= username;
            login.Password = password;
            db.users.Add(login);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted, "Successfully Created");
        }
        [HttpGet]
        [Route("api/Login?username={username}&password={password}")]
        [ActionName("XAMARIN_Login")]
        // GET: api/Login/5  
        public HttpResponseMessage Xamarin_login (string username, string password)
        {
            var login = db.users.Where(x => x.fullName == username && x.Password == password).FirstOrDefault();
            if (login == null)
            {
               return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Enter valid UserName and Password");
            }
            return Request.CreateResponse(HttpStatusCode.Accepted, "Success");
        }
    }
    
}

