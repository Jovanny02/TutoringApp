using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TestController : ApiController
    {
        //Database entity you want to connect to
        TutoringAppDBEntities db = new TutoringAppDBEntities();  
        
        [HttpPost]
        [ActionName("XAMARIN_REG")]
        // POST: api/Login  
        public HttpResponseMessage Xamarin_reg(string username, string password)
        {
            TestLoginEntity login = new TestLoginEntity();
            login.userName = username;
            login.password = password;
            db.TestLoginEntities.Add(login);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted, "Successfully Created");
        }


        [HttpGet]
        [ActionName("XAMARIN_Login")]
        // GET: api/Login/5  
        public HttpResponseMessage Xamarin_login(string username, string password)
        {
            var user = db.TestLoginEntities.Where(x => x.userName == username && x.password == password).FirstOrDefault();
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
