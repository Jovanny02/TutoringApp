using System;
using System.Collections.Generic;
using System.Text;
using ResumeApp.Models;

namespace ResumeApp.ViewModels
{
    class WorkDetailsVM
    {
        public WorkDetailsVM(WorkTile workTile)
        {
            Details = workTile;
        }

        public WorkTile Details { get; set; }

    }
}
