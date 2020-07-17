using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TutoringApp;
using Xamarin.Forms.Xaml;

namespace TutoringApp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            InitializeComponent();
        }

        async private void InfoIcon_Navigate(object sender, EventArgs e)
        {
            //TODO CHANGE TO INFO PAGE WHEN IT IS MADE
            //Potentially change to Mainpage.detail page change versus a navigation stack page change
            await Navigation.PushAsync(new Help());
        }

  
    }
}