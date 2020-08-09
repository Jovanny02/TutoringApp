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
    public partial class Home : BaseContentPage
    {
        public Home()
        {
            //set naviation to viewmodel
            HomeVM VM = new HomeVM();
            VM.Navigation = Navigation;
            BindingContext = VM;
            InitializeComponent();       
        }

    }
}