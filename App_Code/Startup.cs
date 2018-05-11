using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ACHC.Startup))]
namespace ACHC
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
