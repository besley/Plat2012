using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ServiceStack.Text;
using Plat.WebUtility;
using MvcApplication199.Models;

namespace MvcApplication199.Controllers
{
    public class FunctionMenuController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult FuncMenuData()
        {
            var model = GetJsTreeData();
            //return JsonSerializer.SerializeToString(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsTreeModel[] GetJsTreeData()
        {
            var tree = new JsTreeModel[]
            {
                new JsTreeModel { data="Confirm Application", attr=new JsTreeAttribute{ id="10", selected=true}},
                new JsTreeModel
                {
                    data = "Things",
                    attr = new JsTreeAttribute { id="20"},
                    children = new JsTreeModel[]
                    {
                        new JsTreeModel { data = "Thing 1", attr = new JsTreeAttribute {id="21", selected = true}},
                        new JsTreeModel { data = "Thing 2", attr = new JsTreeAttribute {id="22"}},
                        new JsTreeModel { data = "Thing 3", attr = new JsTreeAttribute { id = "23" }},
                        new JsTreeModel
                        {
                            data = "Thing 4",
                            attr = new JsTreeAttribute { id="24"},
                            children = new JsTreeModel[]
                            {
                                new JsTreeModel { data = "Thing 4.1", attr = new JsTreeAttribute { id = "241"}},
                                new JsTreeModel { data = "Thing 4.2", attr = new JsTreeAttribute { id = "242" }},
                                new JsTreeModel { data = "Thing 4.3", attr = new JsTreeAttribute { id = "243" }}
                            }
                        }
                    }

                },
                new JsTreeModel
                {
                    data = "Colors",
                    attr = new JsTreeAttribute { id="40"},
                    children = new JsTreeModel[]
                    {
                        new JsTreeModel { data = "red", attr = new JsTreeAttribute{id="41"}},
                        new JsTreeModel { data = "green", attr = new JsTreeAttribute { id = "42"}},
                        new JsTreeModel { data = "blue", attr = new JsTreeAttribute {id="43"}},
                        new JsTreeModel { data = "yellow", attr = new JsTreeAttribute {id="44"}}
                    }
                }
            };

            return tree;
        }
    }
}