using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TutoringApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaswordRenew : ContentPage
    {
        PasswordRenewVM paswordrenew;
        public PaswordRenew()
        {

            paswordrenew = new PasswordRenewVM();
            paswordrenew.Navigation = Navigation;
            BindingContext = paswordrenew;
            InitializeComponent();

        }

      
    }
}