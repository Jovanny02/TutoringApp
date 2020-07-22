using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorList : ContentPage
    {
        TutorListVM pageVM;
        public TutorList(string searchParam)
        {
            pageVM = new TutorListVM();
            pageVM.SearchQuery = searchParam;
            BindingContext = pageVM;
            InitializeComponent();

        }

        private void LoadTutors(object sender, ItemVisibilityEventArgs e)
        {
            pageVM.LoadTutors(e.Item);
        }
    }
}