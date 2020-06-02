using System;
using System.Collections.Generic;
using System.Text;
using TutoringApp.Models;

namespace TutoringApp.ViewModels
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
