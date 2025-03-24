using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.Configuration;

namespace ChessTrainer
{

  

    public class Startup
    {
      
    }

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

        


          

            RouteConfig.RegisterRoutes(RouteTable.Routes); // Register other application routes
        }
    }
}
