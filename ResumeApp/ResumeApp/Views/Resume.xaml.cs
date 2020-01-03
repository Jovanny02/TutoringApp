using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ResumeApp.Models;

namespace ResumeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Resume : ContentPage
    {
        public List<WorkTile> Jobs { get; set; }
        public Resume()
        {
            InitializeComponent();

            Jobs = new List<WorkTile>();

            Jobs.Add(new WorkTile
            {
                companyName = "JEA",
                fromDate = "May 2019",
                toDate = "Aug 2019",
                iconSrc = "JEA.png",
                jobTitle = "Software Development Intern"
            });

            Jobs.Add(new WorkTile
            {
                companyName = "Chick-Fil-A",
                fromDate = "Jun 2016",
                toDate = "Jul 2017",
                iconSrc = "CFA.png",
                jobTitle = "Team Member"
            });

            Jobs.Add(new WorkTile
            {
                companyName = "Independent Contractor",
                fromDate = "Oct 2012",
                toDate = "Present",
                iconSrc = "whistle.png",
                jobTitle = "Grade 8 Soccer Referee"
            });

            BindingContext = this;
        }

        private void JobSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}