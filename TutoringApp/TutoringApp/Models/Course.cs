using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    public class Course
    {
        public Course()
        {
            this.departmentTitle = string.Empty;
            this.courseName = string.Empty;
        }
        public Course(string departmentTitle, string courseName)
        {
            this.departmentTitle = departmentTitle;
            this.courseName = courseName;        
        }
        public string departmentTitle { get; set; }
        public string courseName { get; set; }
    }




}

