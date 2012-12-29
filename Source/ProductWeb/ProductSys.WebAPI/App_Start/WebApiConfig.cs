using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ServiceStack.Text;

namespace ProductSys.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "defaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new
                {
                    //action = RouteParameter.Optional,
                    id = RouteParameter.Optional
                });
        }
    }
}
