using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SolveChicago.App.Web.Startup))]
namespace SolveChicago.App.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
