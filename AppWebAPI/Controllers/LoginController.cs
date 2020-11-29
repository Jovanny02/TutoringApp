﻿using AppWebAPI.Models;
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
       // [Route("api/Login?username={username}&password={password}")]
        [ActionName("XAMARIN_REG")]
        // POST: api/Login  
        public HttpResponseMessage Xamarin_reg(string username, string password)
        {
            TutoringApp.Models.User appUser = new TutoringApp.Models.User();

            user signup = new user();
            signup.fullName= username;
            signup.Password = password;
            signup.Email = appUser.email;
            signup.UFID = appUser.UFID;     
            
            
           

            Cours coursesSignUp = new Cours();


            coursesSignUp.courseName = appUser.Courses[0].courseName;
            coursesSignUp.departmentTitle = appUser.Courses[0].departmentTitle;
            coursesSignUp.UFID = appUser.UFID;
            

            db.users.Add(signup);
            db.Courses.Add(coursesSignUp);
            db.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.Accepted, "Successfully Created");
        }
        [HttpGet]
        //[Route("api/Login?username={username}&password={password}")]
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

