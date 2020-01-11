using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResumeApp.ViewModels;
using ResumeApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ResumeApp.Views
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