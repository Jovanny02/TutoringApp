using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TutoringApp.ViewModels;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Markup;

namespace TutoringApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : BaseContentPage
    {
        SignUpVM signUpVM = new SignUpVM();
        public SignUp(bool isTutor)
        {
            this.isTutor = isTutor;
            BindingContext = signUpVM;
            signUpVM.Navigation = Navigation;
            InitializeComponent();
            signUpVM.isTutor = isTutor;

            CourseSection.IsVisible = this.isTutor;
            ZoomlinkSection.IsVisible = this.isTutor;
        }

        public bool isTutor = false;

    }
}