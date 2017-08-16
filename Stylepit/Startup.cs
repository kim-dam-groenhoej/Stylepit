using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stylepit.Startup))]
namespace Stylepit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
