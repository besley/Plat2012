using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDialogTutorial.Models;

namespace MvcDialogTutorial.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var p = new Product();
            return PartialView("ProductForm", p);
        }

        public ActionResult Edit(int id)
        {
            var manager = new ProductManager();
            var model = manager.Get(id);
            return PartialView("ProductForm", model);
        }

        public ActionResult List()
        {
            var manager = new ProductManager();
            var model = manager.Get();
            return PartialView("List", model);
        }

        public ActionResult Save(Product product)
        {
            var manager = new ProductManager();
            manager.Save(product);

            var model = manager.Get();
            return PartialView("List", model);
        }
    }
}