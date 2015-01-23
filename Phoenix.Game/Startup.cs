using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(Phoenix.Game.Startup))]

namespace Phoenix.Game
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}