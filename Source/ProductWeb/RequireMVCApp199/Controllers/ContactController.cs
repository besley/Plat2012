using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequireMVCApp199.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult List()
        {
            return View("ContactList");
        }

        public ActionResult Create()
        {
            return View("ContactForm");
        }

        public ActionResult Detail()
        {
            return View("ContactForm");
        }

    }
}
