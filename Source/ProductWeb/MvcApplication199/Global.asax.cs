using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Extensions;


namespace MvcApplication199
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static Container GlobalContainer { get; set; }

        public MvcApplication()
        {
            base.BeginRequest += MvcApplication_BeginRequest;
        }

        protected void MvcApplication_BeginRequest(object sender, EventArgs e)
        {
            var a = "how are you!";
            HttpContext.Current.Items["abc"] = 123;


            
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            BootstrapBundleConfig.RegisterBundles();

            MvcApplication.GlobalContainer = new Container();

            var b = "welcome!";


        }
    }

    internal class Cached
    {
        internal static string Message
        {
            get
            {
                if (HttpContext.Current == null && HttpContext.Current.Items["abc"] == null)
                {
                    HttpContext.Current.Items["abc"] = 123;
                }

                var a = HttpContext.Current.Items["abc"].ToString();
                return a;
            }
        }
    }
}