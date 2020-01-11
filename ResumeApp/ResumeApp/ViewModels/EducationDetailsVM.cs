using System;
using System.Collections.Generic;
using System.Text;
using ResumeApp.Models;
namespace ResumeApp.ViewModels
{
    class EducationDetailsVM
    {
        public EducationDetailsVM(EducationTile educationTile)
        {
            Details = educationTile;
        }

        public EducationTile Details { get;}
    }
}
