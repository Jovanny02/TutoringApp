using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using TutoringApp.Services;
using TutoringApp.Models;

namespace TutoringApp.ViewModels
{
    class SignUpVM : BaseVM
    {
        public SignUpVM()
        {

        }
        private Regex emailRegex = new Regex("^[a-zA-Z]+[a-zA-Z0-9]+[[a-zA-Z0-9-_.!#$%'*+/=?^]{1,20}@[a-zA-Z0-9]{1,20}.[a-zA-Z]{2,3}$");
        private Regex passwordRegex = new Regex("[^a-z0-9]");

        public ICommand SignUpCommand => new Command(async () =>
        {
            if (email == null || email == String.Empty || !emailRegex.IsMatch(email))
            {
                UserDialogs.Instance.Alert("Sign Up Failed: Email format is incorrect", null, null);
                return;
            }
            else if (Name == null || Name == String.Empty)
            {
                UserDialogs.Instance.Alert("Sign Up Failed: Name cannot be empty", null, null);
                return;
            }
            else if (password == null || password == String.Empty || !passwordRegex.IsMatch(password) || password.Length < 8)
            {
                UserDialogs.Instance.Alert("Sign Up Failed: Password must be at least 8 characters long and contain 1 special character", null, null);
                return;
            }
            else if (UFID == null || UFID == String.Empty || UFID.Length !=8 || !IsDigitsOnly(UFID))
            {
                UserDialogs.Instance.Alert("Sign Up Failed: Incorrect UFID format", null, null);
                return;
            }
            else if (isTutor && (Course == null || Course == String.Empty || !helperServices.allCourses.Contains(Course)))
            {
                UserDialogs.Instance.Alert("Sign Up Failed: Invalid course", null, null);
                return;
            }
            else if (isTutor &&
                (zoomLink == null ||
                zoomLink == string.Empty ||
                !Uri.IsWellFormedUriString(zoomLink, UriKind.Absolute) ||
                !zoomLink.Contains("https://ufl.zoom.us")))
            { //TODO add more extensive checks for zoom link
                UserDialogs.Instance.Alert("Sign Up Failed: Invalid zoom link", null, null);
                return;
            }




            User newUser = new User();
            newUser.email = email;
            newUser.password = password;
            int tempUFID;
            //try to parse UFID
            if(Int32.TryParse(UFID, out tempUFID))
            {
                newUser.UFID = tempUFID;
            }
            else
            {
                UserDialogs.Instance.Alert("Sign Up Failed: Invalid UFID", null, null);
                return;
            }
            newUser.name = Name;
            newUser.requestedPay = 15;
            newUser.isTutor = isTutor;
            newUser.AverageRating = 0.0;
            newUser.pictureSrc = "user.png";
            if (isTutor)
            {
                newUser.Courses.Add(new Course { departmentTitle = findDepartment(Course), courseName = Course });
                newUser.zoomLink = zoomLink;
            }

            newUser.ScheduleSections = new List<ScheduleTile>() {
            new ScheduleTile { day = DayOfWeek.Monday, IsUnavailable=true, startTicks = 0, endTicks = 0},
            new ScheduleTile { day = DayOfWeek.Tuesday, IsUnavailable=true, startTicks = 0, endTicks = 0 },
            new ScheduleTile { day = DayOfWeek.Wednesday, IsUnavailable=true, startTicks = 0, endTicks = 0 },
            new ScheduleTile { day = DayOfWeek.Thursday, IsUnavailable=true, startTicks = 0, endTicks = 0 },
            new ScheduleTile { day = DayOfWeek.Friday, IsUnavailable=true, startTicks = 0, endTicks = 0 },
            new ScheduleTile { day = DayOfWeek.Saturday, IsUnavailable=true, startTicks = 0, endTicks = 0 },
            new ScheduleTile { day = DayOfWeek.Sunday, IsUnavailable=true, startTicks = 0, endTicks = 0 }
            };


            try
            {
                UserDialogs.Instance.ShowLoading("Attempting Sign Up");
                bool didCreate = await WebAPIServices.signUpUser(newUser);
                UserDialogs.Instance.HideLoading();
                if (!didCreate)
                {
                    UserDialogs.Instance.Alert("Sign Up Failed: Server error, please try again", null, null);
                    return;
                }

            }
            catch( Exception e)
            {
                UserDialogs.Instance.HideLoading();
                UserDialogs.Instance.Alert("Sign Up Failed: " + e.Message, null, null);
                return;
            }



            UserDialogs.Instance.Alert("Signed In Successfully! ", null, null);
            //pop to root
            await Navigation.PopToRootAsync();


        });

        private string findDepartment(string findCourse)
        {
            foreach(var departmentSection in helperServices.CourseList)
            {
                foreach(var course in departmentSection.courses)
                {
                    if (findCourse == course)
                        return departmentSection.departmentName;
                }

            }

            return "";
        } 


        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }


        public string Course { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public string UFID { get; set; }

        public string zoomLink { get; set; }

        public bool isTutor { get; set; }
    }
}
