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

        public ObservableCollection<EducationSection> EducationSections { get; set; } = new ObservableCollection<EducationSection>();
        public ObservableCollection<ScheduleTile> ScheduleSections { get; set; } = new ObservableCollection<ScheduleTile>();

        //public ObservableCollection<SkillSection> Skills { get; set; } = new ObservableCollection<SkillSection>();
        public ObservableCollection<Course> Courses { get; set; } = new ObservableCollection<Course>();

        public int UFID { get; set; }

        public int requestedPay { get; set; }
        public string Biography { get; set; }
        public string shortBio { get; set; }
        public bool isTutor { get; set; } = false;

        public string zoomLink { get; set; }

    }
}
