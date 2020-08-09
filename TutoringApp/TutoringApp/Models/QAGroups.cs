using System;
using System.Collections.Generic;
using System.Text;

namespace TutoringApp.Models { 

    public class QAGroups : List<QA>
    {
        public string Title { get; set; }
        public string ShortTitle { get; set; }

        public QAGroups(string title, string shortTitle)
        {
            Title = title;
            ShortTitle = shortTitle;

        }
    }
}
