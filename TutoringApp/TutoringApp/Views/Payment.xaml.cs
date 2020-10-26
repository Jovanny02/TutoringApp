using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Payment : BaseContentPage
    {
        PaymentVM paymentVM;

        public Payment()
        {
          
            paymentVM = new PaymentVM();
            paymentVM.Navigation = Navigation;
            BindingContext = paymentVM;
            InitializeComponent();

           
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            paymentVM.PaymentAsync();
        }
    }
}