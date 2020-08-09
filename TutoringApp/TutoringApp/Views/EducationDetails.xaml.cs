using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EducationDetails : BaseContentPage
    {
        public EducationDetails()
        {
            InitializeComponent();
           
        }

        public ICommand SaveCommand;

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            SaveCommand.Execute(sender);
        }
    }
}