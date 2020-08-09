using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models
{
    class TutorInfo
    {
        public string pictureSrc { get; set; } = "user.png";
        public string tutorName { get; set; } = "";

        public string shortDescription { get; set; } = "";

        public string longDescription { get; set; } = "";

        public List<string> tutorTopics { get; set; } = new List<string>{ "" };

        public double averageRating { get; set; } = 0;
        public int displayPosition { get; set; }

    }
}
