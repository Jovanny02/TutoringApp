using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TutoringApp.Models;
using AppWebAPI.Models;
using System.Web;
using System.Text.Json;

namespace AppWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        TutoringAppDBEntities db = new TutoringAppDBEntities();
        // GET api/values/getTutors?searchedCourse={searchedCourse}
        [HttpGet]
        [Route("api/values/getTutors")]
        public HttpResponseMessage getTutors(string searchedCourse)
        {
            try
            {
                 var eligibleTutors = (
                                    from u in db.users
                                    join c in db.Courses
                                    on u.UFID equals c.UFID
                                    where c.courseName == searchedCourse 
                                    && u.isTutor == true
                                    orderby u.averageRating descending
                                    select u).ToList();


                //user tutor = eligibleTutorUFIDs[0].user;
                if (eligibleTutors == null || eligibleTutors.Count < 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "No Tutors Available");
                }
                else
                {
                    List<TutorInfo> returnTutors = new List<TutorInfo>();

                    foreach(var tutor in eligibleTutors)
                    {
                        TutorInfo tempTutor = new TutorInfo();
                        tempTutor.AverageRating = (double)tutor.averageRating;
                        tempTutor.pictureSrc = tutor.pictureSource;
                        tempTutor.isTutor = (bool)tutor.isTutor;
                        tempTutor.requestedPay = (int)tutor.requestedPay;
                        tempTutor.name = tutor.fullName;
                        tempTutor.shortBio = tutor.shortBio;
                        tempTutor.Biography = tutor.Biography;
                        tempTutor.zoomLink = tutor.zoomLink;
                        tempTutor.UFID = tutor.UFID;
                        tempTutor.email = tutor.Email;

                        //get schedules for each tutor
                        foreach(var schedule in tutor.userSchedules)
                        {
                            tempTutor.ScheduleSections.Add(new ScheduleTile
                            {
                                startTicks = (long)schedule.startTicks,
                                endTicks = (long)schedule.endTicks,
                                day = ((DayOfWeek)Enum.Parse(typeof(DayOfWeek), schedule.day, true)),
                                IsUnavailable = (bool)schedule.isUnavailable
                            });
                        }
                        //get tutor courses
                        foreach (var course in tutor.Courses)
                        {
                            tempTutor.Courses.Add(new TutoringApp.Models.Course
                            {
                                departmentTitle = course.departmentTitle,
                                courseName = course.courseName
                            }); ;
                        }

                        //get education sections
                        foreach (var educations in tutor.EducationSections)
                        {
                            tempTutor.EducationSections.Add(new TutoringApp.Models.EducationSection
                            {
                                Major = educations.Major,
                                fromYear = (int)educations.fromYear,
                                toYear = (int)educations.toYear,
                                University = educations.University
                            }); ;
                        }
                        //sort education
                        tempTutor.EducationSections.Sort(delegate (TutoringApp.Models.EducationSection c1, 
                            TutoringApp.Models.EducationSection c2) { return -1 * c1.toYear.CompareTo(c2.toYear); });
                        //sort courses
                        tempTutor.Courses.Sort(delegate (Course c1, Course c2) 
                        { return c1.courseName.CompareTo(c2.courseName); });
                        //sort schedule
                        tempTutor.ScheduleSections = tempTutor.ScheduleSections.OrderBy(x => ((int)x.day)).ToList();


                        returnTutors.Add(tempTutor);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, returnTutors);
                }

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }


        }


        [HttpPost]
        [Route("api/values/updateUser")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> updateUser()
        {
            //ar request = HttpContext.Current.Request;
            try
            {
                HttpContent requestContent = Request.Content;
                string jsonContent = await requestContent.ReadAsStringAsync();
                TutoringApp.Models.User updateUser = JsonSerializer.Deserialize<TutoringApp.Models.User>(jsonContent);

                var user = db.users.Where(x => x.UFID == updateUser.UFID).FirstOrDefault();
                if (user != null)
                {
                    //set user updated properties
                    user.pictureSource = updateUser.pictureSrc;
                    user.isTutor = updateUser.isTutor;
                    user.requestedPay = updateUser.requestedPay;
                    user.fullName = updateUser.name;
                    user.shortBio = updateUser.shortBio;
                    user.Biography = updateUser.Biography;
                    user.zoomLink = updateUser.zoomLink;

                    //delete courses of User 
                    db.Courses.RemoveRange(db.Courses.Where(x => x.UFID == updateUser.UFID));
                    //Add courses
                    foreach(var course in updateUser.Courses)
                    {
                        db.Courses.Add(new Cours
                        {
                            UFID = updateUser.UFID,
                            courseName = course.courseName,
                            departmentTitle = course.departmentTitle,
                            user = user
                        });

                    }
                   // db.Courses.AddRange(dbCourses);


                    //delete education sections
                    db.EducationSections.RemoveRange(db.EducationSections.Where(x => x.UFID == updateUser.UFID));
                    //Add Education sections
                    foreach (var educationSection in updateUser.EducationSections)
                    {
                        db.EducationSections.Add(new AppWebAPI.Models.EducationSection
                        {
                            UFID = updateUser.UFID,
                            Major = educationSection.Major,
                            University = educationSection.University,
                            fromYear = educationSection.fromYear,
                            toYear = educationSection.toYear,
                            user = user
                        });
                    }

                    //delete education sections
                    db.userSchedules.RemoveRange(db.userSchedules.Where(x => x.UFID == updateUser.UFID));
                    //Add Education sections
                    foreach (var daySchedule in updateUser.ScheduleSections)
                    {
                        db.userSchedules.Add(new AppWebAPI.Models.userSchedule
                        {
                            UFID = updateUser.UFID,
                            day = daySchedule.day.ToString(),
                            endTicks = daySchedule.endTicks,
                            startTicks = daySchedule.startTicks,
                            isUnavailable = daySchedule.IsUnavailable,
                            user = user
                        });
                    }


                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Accepted, "Saved Succesfully!");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed,"");


                }

            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }


        }
        

    }
}
