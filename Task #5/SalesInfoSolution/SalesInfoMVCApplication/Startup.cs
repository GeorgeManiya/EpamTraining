using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalesInfoMVCApplication.Startup))]
namespace SalesInfoMVCApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
