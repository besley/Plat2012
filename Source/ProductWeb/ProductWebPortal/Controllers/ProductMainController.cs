using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using ServiceStack.Text;
using Plat.WebUtility;
using ProductSys.BizEntity;

namespace ProductWebPortal.Controllers
{
    public class ProductMainController : WebControllerBase
    {
        public ActionResult Index()
        {
            return PartialView("MainPage");
        }

        public ActionResult DialogForm()
        {
            return View("DialogForm");
        }

        public ActionResult BookDialog()
        {
            return View("BookDialog");
        }

        public ActionResult PaperDialog()
        {
            return View("PaperDialog");
        }
    }
}
