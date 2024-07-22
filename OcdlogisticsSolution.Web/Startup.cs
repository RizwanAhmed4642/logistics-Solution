using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OcdlogisticsSolution.Startup))]
namespace OcdlogisticsSolution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
