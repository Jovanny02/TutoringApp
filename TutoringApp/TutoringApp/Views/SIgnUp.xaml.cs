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
    public partial class SignUp : BaseContentPage
    {
        SignUpVM signUpVM = new SignUpVM();
        public SignUp()
        {
            BindingContext = signUpVM;
            signUpVM.Navigation = Navigation;
            InitializeComponent();
        }
    }
}