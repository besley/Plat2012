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
using SimpleInjector.Advanced;
using Plat.DataRepository;
using Plat.ServiceBuilder.Injection;
using Plat.ServiceBuilder;
using ProductSys.DAL;
using ProductSys.WebAPI.Core;

namespace ProductSys.WebAPI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// WebApi Application
    /// 1. 页面首次访问时，先执行ApplicationStart，后执行BeginRequest；
    /// 2. 页面第二次访问时，直接执行BeginRequest；
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        public WebApiApplication()
        {
            base.BeginRequest += WebApiApplication_BeginRequest;
            base.EndRequest += WebApiApplication_EndRequest;
        }

        /// <summary>
        /// 页面请求开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WebApiApplication_BeginRequest(object sender, EventArgs e)
        {
            ApplicationMediator.BeginRequest();
        }

        /// <summary>
        /// web request结束事件，实现dbcontext的保存和资源释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WebApiApplication_EndRequest(object sender, EventArgs e)
        {
            ApplicationMediator.EndRequest();
        }

        /// <summary>
        /// 应用程序开始，注册路由，注册Ioc等。
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //use Json format 
            SetJsonFormat();
        }

        /// <summary>
        /// 设置JSon格式
        /// </summary>
        private void SetJsonFormat()
        {
            var config = GlobalConfiguration.Configuration;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}