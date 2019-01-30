using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StackOverflow.Startup))]
namespace StackOverflow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
