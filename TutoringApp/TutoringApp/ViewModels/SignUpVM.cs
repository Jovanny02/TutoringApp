using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using TutoringApp.Services;

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




            //TODO create sign up call using information in password, user, and email

            //pop to root
            await Navigation.PopToRootAsync();


        });


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
