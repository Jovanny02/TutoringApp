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
    public partial class WhoAreYou : ContentPage
    {
        WhoAreYouVM whoareyouVM;
        public WhoAreYou()
        {
            InitializeComponent();
            whoareyouVM = new WhoAreYouVM();
            whoareyouVM.Navigation = Navigation;
            BindingContext = whoareyouVM;
        }
    }
}