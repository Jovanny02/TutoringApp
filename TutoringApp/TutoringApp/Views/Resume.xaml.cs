using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TutoringApp.ViewModels;
using TutoringApp.Models;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Resume : ContentPage
    {
        public Resume()
        {
            InitializeComponent();
            BindingContext = new ResumeVM();
        }

        async private void JobSelected(object sender, SelectedItemChangedEventArgs e)
        {
            WorkTile tile = ((WorkTile)e.SelectedItem);
            if(tile != null)
            {
                await Navigation.PushAsync(new ResumeDetails(tile));
            }
            JobList.SelectedItem = null;
        }

        async private void EducationSelected(object sender, SelectedItemChangedEventArgs e)
        {
            EducationTile tile = ((EducationTile)e.SelectedItem);
            if (tile != null)
            {
                await Navigation.PushAsync(new EducationDetails(tile));
            }
            EducationList.SelectedItem = null;
        }

        private void SkillList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            SkillList.SelectedItem = null;
        }
    }
}