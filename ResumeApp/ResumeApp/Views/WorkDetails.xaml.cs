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
    public partial class ResumeDetails : ContentPage
    {
        public ResumeDetails(WorkTile workTile)
        {
            InitializeComponent();
            BindingContext = new WorkDetailsVM(workTile);
        }

    }
}