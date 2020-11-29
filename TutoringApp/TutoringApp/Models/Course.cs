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


        public override bool Equals(object obj)
        {
            // Check for null  
            if (ReferenceEquals(obj, null))
                return false;
            // Check for same reference  
            if (ReferenceEquals(this, obj))
                return true;
            var person = (Course)obj;
            return (this.departmentTitle == person.departmentTitle && this.courseName == person.courseName);
        }
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + departmentTitle.GetHashCode();
                hash = hash * 23 + courseName.GetHashCode();
                return hash;
            }
        }


    }



}

