using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SandBox.Startup))]
namespace SandBox
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
