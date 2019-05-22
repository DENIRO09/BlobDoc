using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppDev3A.Startup))]
namespace AppDev3A
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
