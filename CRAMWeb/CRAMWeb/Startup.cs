using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRAMWeb.Startup))]
namespace CRAMWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
