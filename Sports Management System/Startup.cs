using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sports_Management_System.Startup))]
namespace Sports_Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
