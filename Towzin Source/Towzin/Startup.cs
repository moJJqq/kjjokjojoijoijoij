using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Towzin.Startup))]
namespace Towzin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
