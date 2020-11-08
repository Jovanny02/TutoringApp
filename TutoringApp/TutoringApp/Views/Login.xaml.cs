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
    public partial class Login : BaseContentPage
    {
        
        LoginVM loginVM;
        
        public Login()
        {
            loginVM = new LoginVM();
            loginVM.Navigation = Navigation;
            BindingContext = loginVM;
            InitializeComponent();
        }
    }
}