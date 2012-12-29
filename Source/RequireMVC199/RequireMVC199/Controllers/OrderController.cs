using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using ServiceStack.Text;
using Plat.WebUtility;
using RequireMVC199.RequireJS;
using ProductSys.BizEntity;

namespace RequireMVC199.Controllers
{
    public class OrderController : RequireJSController
    {
        public OrderController()
        {
            base.ApiUrl = "http://localhost:8081/ProductSys.WebAPI/api/OrderView";
        }

        public ActionResult List()
        {
            return View("OrderList");
        }

        public ActionResult Create()
        {
            ViewData["ORDER_ADD_OR_EDIT"] = "A";
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
            ViewData["ORDER_ADD_OR_EDIT"] = "E";
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
    }
}
