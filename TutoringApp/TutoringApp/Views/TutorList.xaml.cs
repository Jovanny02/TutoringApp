using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorList : BaseContentPage
    {
        TutorListVM pageVM;
        public TutorList(string searchParam)
        {
            pageVM = new TutorListVM();
            pageVM.SearchQuery = searchParam;
            BindingContext = pageVM;
            InitializeComponent();

            //set sizing for circular picture
            pictureSize = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) * (0.1);
            radius = pictureSize / 2;

        }

        public Double pictureSize { get; set; }
        public int EditLabelSize { get; set; }
        public Double radius { get; set; }


        // private void LoadTutors(object sender, ItemVisibilityEventArgs e)
        // {
        //     pageVM.LoadTutors.Execute(e.Item);
        //  }

        private void TutorSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count < 1 || e.CurrentSelection[0] == null)
                return;

            Navigation.PushAsync(new TutorView(e.CurrentSelection[0]));
            ListedTutors.SelectedItem = null;
        }
    }
}