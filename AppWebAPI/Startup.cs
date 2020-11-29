using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AppWebAPI.StartupOwin))]

namespace AppWebAPI
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
