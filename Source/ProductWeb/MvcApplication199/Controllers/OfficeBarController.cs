using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using SimpleInjector;
using SimpleInjector.Extensions;
using MvcApplication199;
using MvcApplication199.Models;

namespace MvcApplication199.Controllers
{
    public class OfficeBarController : Controller
    {
        public ActionResult TabBar()
        {
            return View();
        }

        public ActionResult ToolBar()
        {
            return View();
        }

        public ActionResult FBox()
        {
            //DbContext context = new DbContext("abc");
            //var container = MvcApplication.GlobalContainer;
            //var containerWrapper = new ContainerWrapper(container);

            //container.Register<IProductFly>(()=>{
            //    var fly = new ProductFly();
            //    fly.InitializeService(containerWrapper, context);
            //    return fly;
            //});

            //var productFly = container.GetInstance<IProductFly>();
            //string message = productFly.GetMessage("Mrs Wang");
            var message = Cached.Message;
            return View();
        }

        public ActionResult SeaJs()
        {
            return View();
        }
    }
}
