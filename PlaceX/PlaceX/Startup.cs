using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlaceX.Startup))]
namespace PlaceX
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
