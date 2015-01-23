using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(Phoenix.MudGame.Startup))]

namespace Phoenix.MudGame
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}