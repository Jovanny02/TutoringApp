using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class stripeInformationPage : ContentPage
    {
        public ICommand tapCardCommand;
        public ICommand saveStripeCommand;

        public stripeInformationPage()
        {
            InitializeComponent();
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            saveStripeCommand.Execute(this.BindingContext);
        }

        private void Card_Tapped(object sender, EventArgs e)
        {
            tapCardCommand.Execute(sender);
        }

        public void updateCardView(string cardNum, string expirationDate, string CVV)
        {
            expirationSpan.Text = expirationDate;
            cardNumberSpan.Text = cardNum;
            CVVSpan.Text = CVV;
        }



    }







}