using System;
using System.Collections.Generic;
using System.Text;
using TutoringApp.Models;
namespace TutoringApp.ViewModels
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
