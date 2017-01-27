using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SolveChicago.App.Startup))]
namespace SolveChicago.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
