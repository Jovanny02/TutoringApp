using AppWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TutoringApp.Models;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;

namespace AppWebAPI.Controllers
{
    public class LoginController : ApiController
    {
        TutoringAppDBEntities db = new TutoringAppDBEntities();
        /*
         * Helper Functions
         */
        byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }
            return bytes;
        }

        byte[] GenerateHash(string password, byte[] salt, int iterations, int length)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return deriveBytes.GetBytes(length);
            }
        }




        [HttpPost]
        [Route("api/Login/UserSignUp")]
        [ActionName("XAMARIN_Sign_Up")]
        // POST: api/Login  
        public async System.Threading.Tasks.Task<HttpResponseMessage> UserSignUp()
        {
            try
            {
                HttpContent requestContent = Request.Content;
                string jsonContent = await requestContent.ReadAsStringAsync();
                TutoringApp.Models.User newUser = JsonSerializer.Deserialize<TutoringApp.Models.User>(jsonContent);

                //generate salt 
                byte[] salt1 = GenerateSalt(8);
                //generate Hash
               // byte[] passwordBytes = new System.Text.UTF8Encoding(false).GetBytes(newUser.password); //Encoding.ASCII.GetBytes(newUser.password);
                byte[] hash = GenerateHash(newUser.password, salt1, 1000, 28);
                //new System.Text.UTF8Encoding(false).GetBytes(data1);
                //set user password equal to the hash
                newUser.password = Convert.ToBase64String(hash);
                string salt = Convert.ToBase64String(salt1);


                //add User
                db.users.Add(new user
                {
                    pictureSource = newUser.pictureSrc,
                    Email = newUser.email,
                    UFID = newUser.UFID,
                    averageRating = 0.0,
                    fullName = newUser.name,
                    zoomLink = newUser.zoomLink,
                    Password = newUser.password,
                    isTutor = newUser.isTutor,
                    requestedPay = newUser.requestedPay,
                    stripeAccountID = newUser.stripeAccountID,
                    
                    salt = salt
                });
                // add users course
                if (newUser.isTutor && newUser.Courses.Count != 0)
                {
                    db.Courses.Add(new Cours
                    {
                        UFID = newUser.UFID,
                        departmentTitle = newUser.Courses[0].departmentTitle,
                        courseName = newUser.Courses[0].courseName
                    });
                }
                db.SaveChanges();
                //get user  and then add all of their schedule
                var newDBUser = db.users.Where(x => x.UFID == newUser.UFID).FirstOrDefault();
                foreach(var schedule in newUser.ScheduleSections)
                {
                    db.userSchedules.Add(new userSchedule
                    {
                        UFID = newUser.UFID,
                        day =  schedule.day.ToString(),
                        endTicks = schedule.endTicks,
                        startTicks = schedule.startTicks,
                        isUnavailable = schedule.IsUnavailable,
                        user = newDBUser
                    });
                }
              

                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Successfully Created");
            }                       
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }

        }

        [HttpGet]
        //[Route("api/Login?UFID={UFID}&password={password}")]
        [ActionName("XAMARIN_Login")]
        public HttpResponseMessage UserLogin(int UFID, string password)
        {

            var user = db.users.Where(x => x.UFID == UFID).FirstOrDefault();
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Enter valid UserName and Password");
            }
            else
            {
                //generate salt 
                byte[] salt1 = Convert.FromBase64String(user.salt);
                //generate Hash
              //  byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
                byte[] hash = GenerateHash(password, salt1, 1000, 28);

                //set user password equal to the hash
                string testPass = Convert.ToBase64String(hash);

                if(testPass != user.Password)
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Please Enter valid UserName and Password");
                }


                //parse User into an appUser
                TutoringApp.Models.User appUser = new TutoringApp.Models.User();
                appUser.AverageRating = (double)user.averageRating;
                appUser.Biography = user.Biography;
                appUser.email = user.Email;
                appUser.isTutor = (bool)user.isTutor;
                appUser.name = user.fullName;
                appUser.password = user.Password;
                appUser.pictureSrc = user.pictureSource;
                appUser.requestedPay = (int)user.requestedPay;
                appUser.shortBio = user.shortBio;
                appUser.zoomLink = user.zoomLink;
                appUser.UFID = user.UFID;
                appUser.stripeAccountID = user.stripeAccountID;


                //get additional information
                var schedules = db.userSchedules.AsNoTracking().Where(x => x.UFID == user.UFID).ToList();
                var courses = db.Courses.AsNoTracking().Where(x => x.UFID == user.UFID).OrderByDescending(x => x.courseName).ToList();
                var educations = db.EducationSections.AsNoTracking().Where(x => x.UFID == user.UFID).OrderByDescending(x => x.toYear).ToList();

                //parse information list
                foreach (var schedule in schedules)
                {
                    appUser.ScheduleSections.Add(new ScheduleTile
                    {
                        startTicks = (long)schedule.startTicks,
                        endTicks = (long)schedule.endTicks,
                        day = ((DayOfWeek)Enum.Parse(typeof(DayOfWeek), schedule.day, true)),
                        IsUnavailable = (bool)schedule.isUnavailable
                }); ;
                }

                foreach (var course in courses)
                {
                    appUser.Courses.Add(new TutoringApp.Models.Course
                    {
                        departmentTitle = course.departmentTitle,
                        courseName = course.courseName
                    }); ;
                }

                for(int i = 0; i <educations.Count; i++)
                {                  
                    appUser.EducationSections.Add(new TutoringApp.Models.EducationSection
                    {
                        Major = educations[i].Major,
                        fromYear = (int)educations[i].fromYear,
                        toYear = (int)educations[i].toYear,
                        University = educations[i].University,
                        key = i
                    }); ;
                }
                //sort days properly
                appUser.ScheduleSections = appUser.ScheduleSections.OrderBy(x => ((int)x.day)).ToList();

                return Request.CreateResponse(HttpStatusCode.Accepted, appUser);
            }
        }




    }
}
