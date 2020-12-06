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

        /*
         * get a tutor's reservations
         * 
         */
        [HttpGet]
        [Route("api/values/getTutorReservations")]
        public HttpResponseMessage getTutorReservations(string tutorUFID)
        {
            try
            {
                int UFID; 
                    
                bool didParseUFID = Int32.TryParse(tutorUFID,out UFID);

                if (!didParseUFID)
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Could not parse tutor UFID");
                }

                var tutorReservations = (
                                   from r in db.reservations
                                   where r.tutorUFID == UFID
                                   && r.fromDateTime > DateTime.Now
                                   && r.isCancelled == false
                                   select r).ToList();

                List<TutoringApp.Models.Reservation> reservations = new List<TutoringApp.Models.Reservation>();

                //user tutor = eligibleTutorUFIDs[0].user;
                if (tutorReservations == null || tutorReservations.Count < 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, reservations);
                }
                else
                {
                    foreach (var reservation in tutorReservations)
                    {
                        reservations.Add(new Reservation
                        {
                            fromDate = reservation.fromDateTime,
                            toDate = reservation.toDateTime,
                            tutorUFID = reservation.tutorUFID,
                            studentUFID = reservation.studentUFID,
                            isCanceled = (bool)reservation.isCancelled                        
                        });
                    }              
                    return Request.CreateResponse(HttpStatusCode.OK, reservations);
                }

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }
        }


        /*
        * get a Tutor's ReservationTiles
        * 
        */


        [HttpGet]
        [Route("api/values/getTutorReservationTiles")]
        public HttpResponseMessage getTutorReservationTiles(string tutorUFID)
        {
            try
            {
                int UFID;

                bool didParseUFID = Int32.TryParse(tutorUFID, out UFID);

                if (!didParseUFID)
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Could not parse tutor UFID");
                }

                var StudentReservations = (
                                 from r in db.reservations
                                 where r.tutorUFID == UFID
                                 && r.isCancelled == false
                                 orderby r.toDateTime descending
                                 select r).ToList();
                List<TutoringApp.Models.ReservationTile> reservations = new List<TutoringApp.Models.ReservationTile>();

                if (StudentReservations == null || StudentReservations.Count < 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, reservations);
                }

                else
                {

                    foreach (var reservation in StudentReservations)
                    {
                        reservations.Add(new ReservationTile
                        {
                            //user1=tutor    user=student
                            fromDate = reservation.fromDateTime,
                            toDate = reservation.toDateTime,
                            tutorUFID = reservation.tutorUFID,
                            studentUFID = reservation.studentUFID,
                            isCanceled = (bool)reservation.isCancelled,
                            isCompleted = (bool)reservation.isCompleted,
                            tutorName = reservation.user1.fullName,
                            studentName = reservation.user.fullName,
                            studentPicture = reservation.user.pictureSource,
                            zoomLink = reservation.user1.zoomLink,

                        });

                    }

                    return Request.CreateResponse(HttpStatusCode.OK, reservations);

                }

            }

            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }

        }






        /*
        * get a student's reservations
        * 
        */

        [HttpGet]
        [Route("api/values/getStudentReservations")]
        public HttpResponseMessage getStudentReservations(string studentUFID)
        {
            try
            {
                int UFID;

                bool didParseUFID = Int32.TryParse(studentUFID, out UFID);

                if (!didParseUFID)
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Could not parse tutor UFID");
                }

                var StudentReservations = (
                                 from r in db.reservations
                                 where r.studentUFID == UFID
                                 && r.isCancelled == false
                                 orderby r.toDateTime descending
                                 select r).ToList();
                List<TutoringApp.Models.ReservationTile> reservations = new List<TutoringApp.Models.ReservationTile>();

                if (StudentReservations == null || StudentReservations.Count < 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, reservations);
                }

                else
                {

                    foreach (var reservation in StudentReservations)
                    {
                        if(!((bool)reservation.isCompleted) && (double)reservation.tutorRating == 0.0)
                        {
                            reservation.tutorRating = 2.5;
                        }

                        reservations.Add(new ReservationTile
                        {
                            //user 1 tutor user student
                            fromDate = reservation.fromDateTime,
                            toDate = reservation.toDateTime,
                            tutorUFID = reservation.tutorUFID,
                            studentUFID = reservation.studentUFID,
                            isCanceled = (bool)reservation.isCancelled,
                            isCompleted = (bool)reservation.isCompleted,
                            tutorName = reservation.user1.fullName,
                            studentName= reservation.user.fullName,
                            tutorPicture= reservation.user1.pictureSource,
                            zoomLink= reservation.user1.zoomLink,
                            rating= (double)reservation.tutorRating

                          
                        });

                    }

                    return Request.CreateResponse(HttpStatusCode.OK, reservations);

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

        /*
         * Set Reservation Request
         * 
         */
        [HttpPost]
        [Route("api/values/setReservations")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> setReservations()
        {
            //ar request = HttpContext.Current.Request;
            try
            {
                HttpContent requestContent = Request.Content;
                string jsonContent = await requestContent.ReadAsStringAsync();
                List<TutoringApp.Models.Reservation> reservationList = JsonSerializer.Deserialize<List<TutoringApp.Models.Reservation>>(jsonContent);


                if(reservationList == null || reservationList.Count < 1)
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "No Reservations to reserve");
                }

                //USER1 corresponds to TutorUFID
                //USER correseponds to StudentUFID

                int tutorUFID = reservationList[0].tutorUFID;
                int studentUFID = reservationList[0].studentUFID;

                var tutor = db.users.Where(x => x.UFID == tutorUFID).FirstOrDefault();
                var student = db.users.Where(x => x.UFID == studentUFID).FirstOrDefault();

                if (tutor == null || student == null)
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "Student or tutor not found");
                }

                if (reservationList[0].tutorUFID == reservationList[0].studentUFID)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, "Tutor and student are the same");
                }

                //Add list of reservations
                foreach (var reservation in reservationList)
                {
                    db.reservations.Add(new reservation { 
                        studentUFID = reservation.studentUFID,
                        tutorUFID = reservation.tutorUFID,
                        fromDateTime = reservation.fromDate,
                        toDateTime = reservation.toDate,
                        tutorRating = 0.0,
                        isCancelled = false,
                        isCompleted = false,
                        user1 = (user)tutor,
                        user = (user)student
                        }
                    );

                }


                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Saved Succesfully!");       
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }


        }

        [HttpPut]
        [Route("api/values/submitRating")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> submitRating()
        {
            try
            {
                HttpContent requestContent = Request.Content;
                string jsonContent = await requestContent.ReadAsStringAsync();
                TutoringApp.Models.ReservationTile selectedReservation = JsonSerializer.Deserialize<TutoringApp.Models.ReservationTile>(jsonContent);
                if (selectedReservation == null )
                {
                    return Request.CreateResponse(HttpStatusCode.ExpectationFailed, "No selected Reservation");
                }

                var reservation = db.reservations.Where(x => 
                x.studentUFID == selectedReservation.studentUFID &&
                x.tutorUFID == selectedReservation.tutorUFID &&
                x.toDateTime == selectedReservation.toDate &&
                x.fromDateTime == selectedReservation.fromDate
                ).FirstOrDefault();

                //submit the rating and mark it as completed
                reservation.tutorRating = Math.Truncate(100 * selectedReservation.rating) / 100;
                reservation.isCompleted = true;



                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Accepted, "Saved Succesfully!");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, e.Message);
            }


        }



    }
}
