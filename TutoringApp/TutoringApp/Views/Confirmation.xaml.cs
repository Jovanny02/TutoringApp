using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using TutoringApp.ViewModels;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Confirmation : BaseContentPage
    {

        private ConfirmationVM confirmVM;
        public Confirmation(object tutor, object reservations)
        {
            confirmVM = new ConfirmationVM(tutor, reservations);
            InitializeComponent();
            confirmVM.Navigation = this.Navigation;
            this.BindingContext = confirmVM;
        }

    }
}