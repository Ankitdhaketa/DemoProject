using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Boat_Rent.Startup))]
namespace Boat_Rent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
