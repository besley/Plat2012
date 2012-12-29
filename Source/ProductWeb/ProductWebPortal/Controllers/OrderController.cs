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
    public class OrderController : WebControllerBase
    {
        public OrderController()
        {
            base.ApiUrl = "http://localhost:8081/ProductSys.WebAPI/api/OrderView";
        }

        public ActionResult Create()
        {
            ViewData["CRUD_ORDER"] = "C";
            var model = new OrderView();
            return PartialView("OrderForm", model);
        }

        /// <summary>
        /// 创建订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(OrderView entity)
        {
            HttpClient httpClient = HttpClientHelper.Create(base.ApiUrl);
            var jsonValue = JsonSerializer.SerializeToString<OrderView>(entity);
            string result = httpClient.Insert(jsonValue);

            return RedirectToAction("List", "Order");
        }

        public ActionResult List()
        {
            return View("OrderList");
        }

        /// <summary>
        /// 更新订单数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(OrderView entity)
        {
            IList<OrderView> entityList = new List<OrderView>();
            entityList.Add(entity);

            return UpdateBatch(entityList);
        }

        /// <summary>
        /// 在表格里面编辑的多行提交
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateBatch(IList<OrderView> entityList)
        {
            HttpClient httpClient = HttpClientHelper.Create(base.ApiUrl);
            var jsonValue = JsonSerializer.SerializeToString<IList<OrderView>>(entityList);
            string result = httpClient.Update(jsonValue);

            return RedirectToAction("List", "Order");
        }

        /// <summary>
        /// 查看订单数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[HttpGet]
        //public ActionResult Detail(string id)
        //{
        //    string url = base.ApiUrl + "/Get/" + id;
        //    HttpClient httpClient = HttpClientHelper.Create(url);
        //    string result = httpClient.GetString();
        //    var model = JsonSerializer.DeserializeFromString<OrderView>(result);
        //    ViewData["CRUD_ORDER"] = "U";
        //    return PartialView("OrderForm", model);
        //}

        /// <summary>
        /// 查看订单数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(string id)
        {
            string url = base.ApiUrl + "/Get/" + id;
            HttpClient httpClient = HttpClientHelper.Create(url);
            string result = httpClient.GetString();
            OrderView model = null;
            if (!string.IsNullOrEmpty(result) && result != "null")
                model = JsonSerializer.DeserializeFromString<OrderView>(result);
            ViewData["CRUD_ORDER"] = "U";
            return PartialView("OrderForm", model);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(IList<string> ids)
        {
            string id = ids[0];
            string url = base.ApiUrl + "/" + id;
            HttpClient httpClient = HttpClientHelper.Create(url);
            string result = httpClient.Delete();

            return RedirectToAction("List", "Order");
        }

        #region test code
        public ActionResult Index404()
        {
            return View();
        }

        public ActionResult Index10()
        {
            return View();
        }



        public ActionResult SGExample()
        {
            return View();
        }

        public ActionResult UITest()
        {
            return View();
        }

        public ActionResult Index_SlickGridGood()
        {
            return View();
        }



        public ActionResult LayoutTest()
        {
            return View();
        }

        public ActionResult ListAll()
        {
            HttpClient httpClient = HttpClientHelper.Create(base.ApiUrl);
            string result = httpClient.GetString();

            return PartialView("OrderList", result);
        }

        public ActionResult BootInput()
        {
            return View();
        }

        public ActionResult EasyWindow()
        {
            return View();
        }
        #endregion
    }
}
