using Microsoft.Owin;
using Owin;
using Startup = WebUI.Startup;

[assembly: OwinStartup(typeof(Startup))]
namespace WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
