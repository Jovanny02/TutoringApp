using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TutoringApp.Models
{
    public class User
    {
        public User()
        {

        }

        public string pictureSrc { get;  set; } = "user.png";

        public double AverageRating { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public List<EducationSection> EducationSections { get; set; } = new List<EducationSection>();
        public List<ScheduleTile> ScheduleSections { get; set; } = new List<ScheduleTile>();

        //public ObservableCollection<SkillSection> Skills { get; set; } = new ObservableCollection<SkillSection>();
        public List<Course> Courses { get; set; } = new List<Course>();

        public int UFID { get; set; }

        public int requestedPay { get; set; }
        public string Biography { get; set; }
        public string shortBio { get; set; }
        public bool isTutor { get; set; } = false;

        public string zoomLink { get; set; }

    }
}
