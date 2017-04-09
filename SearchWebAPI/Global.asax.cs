
using Search.Repository.Context;
using SearchWebAPI.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace SearchWebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<SearchContext>(new SearchDataInitializer());
            UnityWebApiActivator.Start();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
