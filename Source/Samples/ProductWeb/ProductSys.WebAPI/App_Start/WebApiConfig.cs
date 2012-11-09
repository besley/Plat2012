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
          //  config.Formatters.JsonFormatter.SerializerSettings.ContractResolver 
          //= new CamelCasePropertyNamesContractResolver(); 


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "actionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new
                {
                    action = RouteParameter.Optional,
                    id = RouteParameter.Optional
                });

            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = 
            //    new came

        }
    }
}
