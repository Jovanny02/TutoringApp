using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringApp.ViewModels;
using TutoringApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EducationDetails : ContentPage
    {
        public EducationDetails(EducationTile educationTile)
        {
            InitializeComponent();
            BindingContext = new EducationDetailsVM(educationTile);
        }
    }
}