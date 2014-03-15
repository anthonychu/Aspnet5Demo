using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aspnet5Demo.Startup))]
namespace Aspnet5Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
