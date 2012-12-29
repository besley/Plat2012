using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequireMVC199.Controllers
{
    public class SubController : Controller
    {
        //
        // GET: /Sub/

        public ActionResult Index()
        {
            return View("SubIndex");
        }

    }
}
