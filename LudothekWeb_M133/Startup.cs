using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LudothekWeb_M133.Startup))]
namespace LudothekWeb_M133
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
