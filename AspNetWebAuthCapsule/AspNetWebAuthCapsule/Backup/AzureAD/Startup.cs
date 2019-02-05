using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspNetWebAuthCapsule.Startup))]
namespace AspNetWebAuthCapsule
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
