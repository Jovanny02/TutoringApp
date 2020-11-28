using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    public class EducationSection
    {
        public EducationSection()
        {
            key = int.MaxValue;
        }

        public EducationSection(EducationSection copySection)
        {
            Major = copySection.Major;
            fromYear = copySection.fromYear;
            toYear = copySection.toYear;
            University = copySection.University;
            key = copySection.key;
        }

        public string Major { get; set; }

        public int fromYear { get; set; } = DateTime.Now.Year;

        public int toYear { get; set; } = DateTime.Now.Year;

        public string University { get; set; }
        public string fullDate
        {
            get
            {
                return string.Format("{0} - {1}", fromYear, toYear);
            }
        }

        public int key { get; set; }

        public override bool Equals(object obj)
        {
            // Check for null  
            if (ReferenceEquals(obj, null))
                return false;
            // Check for same reference  
            if (ReferenceEquals(this, obj))
                return true;
            var tempSection = (EducationSection)obj;
            return (this.key == tempSection.key);
        }
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + Major.GetHashCode();
                hash = hash * 23 + fromYear.GetHashCode();
                hash = hash * 23 + toYear.GetHashCode();
                hash = hash * 23 + University.GetHashCode();
                return hash;
            }
        }
    }



}
