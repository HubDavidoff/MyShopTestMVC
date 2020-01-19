using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shop.WebUI.Startup))]
namespace Shop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
