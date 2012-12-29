using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using ServiceStack.Text;
using Plat.WebUtility;
using ProductSys.BizEntity;

namespace ProductWebPortal.Controllers
{
    public class BarController : WebControllerBase
    {
        public ActionResult TabBar()
        {
            return View();
        }

        public ActionResult ToolBar()
        {
            return View();
        }
    }
}
