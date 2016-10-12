using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Phosto.Startup))]
namespace Phosto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
