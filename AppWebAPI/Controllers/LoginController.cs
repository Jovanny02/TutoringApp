using AppWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppWebAPI.Controllers
{
    public class LoginController : ApiController
    {
        TutoringAppDBEntities db = new TutoringAppDBEntities();  
         
        [HttpPost]
        [ActionName("XAMARIN_REG")]
        // POST: api/Login  
        public HttpResponseMessage Xamarin_reg(string username, string password)
        {
            loginTest login = new loginTest();
            login.username = username;
            login.password = password;
            db.loginTests.Add(login);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted, "Successfully Created");
        }
        [HttpGet]
        [ActionName("XAMARIN_Login")]
        // GET: api/Login/5  
        public HttpResponseMessage Xamarin_login(string username, string password)
        {
            var user = db.loginTests.Where(x => x.username == username && x.password == password).FirstOrDefault();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Enter valid UserName and Password");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, "Success");
            }
        }
    }
}
