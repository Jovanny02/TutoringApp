using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    public class groupedCourses
    {
        public string departmentName { get; set; }
        public List<string> courses { get; set; } = new List<string>();
    }
}
