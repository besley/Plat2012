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
    public class MainController : WebControllerBase
    {
        public MainController()
        {
            base.ApiUrl = "http://localhost:8081/ProductSys.WebAPI/api/FunctionMenu";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }
    }
}