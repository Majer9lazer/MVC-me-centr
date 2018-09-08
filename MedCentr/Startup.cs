using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedCentr.Startup))]
namespace MedCentr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
