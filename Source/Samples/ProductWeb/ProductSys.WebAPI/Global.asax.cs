using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Extensions;
using Plat.DataRepository;
using ProductSys.DAL;
using ProductSys.WebAPI.Models;



namespace ProductSys.WebAPI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        public static readonly string _DbContextName = "ProductDbContextInstance";

        public static Container GlobalContainer { get; private set; }

        public WebApiApplication()
        {
            base.BeginRequest += WebApiApplication_BeginRequest;
            base.EndRequest += WebApiApplication_EndRequest;
        }

        protected void WebApiApplication_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Items[_DbContextName] = new ProductDbContext();
        }

        /// <summary>
        /// web request结束事件，实现dbcontext的保存和资源释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WebApiApplication_EndRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Items[_DbContextName] != null)
            {
                ProductDbContext dbContext = (ProductDbContext)HttpContext.Current.Items[_DbContextName];
                if (dbContext != null)
                {
                    //数据库保存提交：per request one dbcontext
                    dbContext.SaveChanges();
                    dbContext.Dispose();
                }
            }
        }

        /// <summary>
        /// 应用程序开始，注册路由，注册Ioc等。
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //regiester IoC container
            var container = new Container();
            container.Register<IProductRepository, ProductRepository>();
            container.Register<IOrderRepository, OrderRepository>();
            
            WebApiApplication.GlobalContainer = container;

            //use Json format 
            var config = GlobalConfiguration.Configuration;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }       
    }

    /// <summary>
    /// 缓存在httpcontext对象中的dbcontext
    /// </summary>
    public class DbContextCached
    {
        internal static ProductDbContext Current
        {
            get
            {
                return HttpContext.Current.Items[WebApiApplication._DbContextName] as ProductDbContext;
            }
        }
    }
}