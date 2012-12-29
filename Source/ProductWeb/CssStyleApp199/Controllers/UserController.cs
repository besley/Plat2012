using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CssStyleApp199.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult List()
        {
            return View("UserList");
        }

        public ActionResult Add()
        {
            return View("UserAdd");
        }

    }
}
