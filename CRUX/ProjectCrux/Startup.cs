using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectCrux.Startup))]
namespace ProjectCrux
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
