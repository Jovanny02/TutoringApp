using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutoringApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using Acr.UserDialogs;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Payment : BaseContentPage
    {
        public ICommand saveCommand;
        public Payment()
        {
            InitializeComponent();           
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            saveCommand.Execute(this.BindingContext);
        }
    }
}