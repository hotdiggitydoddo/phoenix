using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Phoenix.Game
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MudGame.Instance.GetHashCode();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
    }
}
