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

namespace ProductWebPortal.Controllers
{
    public class StorageCheckController : RequireJSController
    {
        public StorageCheckController()
        {
            base.ApiUrl = "http://platweb/ProductSys.WebAPI/api/StorageCheck";
        }

        //
        // GET: /StockCheck/

        public ActionResult List()
        {
            return View("StorageCheckList");
        }

        public ActionResult Create()
        {
            ViewData["STORAGECHECK_ADD_OR_EDIT"] = "A";
            var model = new StorageCheck();
            return View("StorageCheckForm", model);
        }

        /// <summary>
        /// 创建库存数据
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(StorageCheck entity)
        {
            HttpClient httpClient = HttpClientHelper.Create(base.ApiUrl);
            var jsonValue = JsonSerializer.SerializeToString<StorageCheck>(entity);
            string result = httpClient.Insert(jsonValue);

            return RedirectToAction("Index", "StorageCheck");
        }

        /// <summary>
        /// 适用于表单单条记录编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(StorageCheck entity)
        {
            HttpClient httpClient = HttpClientHelper.Create(base.ApiUrl);
            IList<StorageCheck> entityList = new List<StorageCheck>();
            entityList.Add(entity);

            var jsonValue = JsonSerializer.SerializeToString<IList<StorageCheck>>(entityList);
            ViewData["U_StorageCheck_Rows"] = jsonValue;
            string result = httpClient.Update(jsonValue);

            return RedirectToAction("Index", "StorageCheck");
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            string url = base.ApiUrl + "/Get/" + id;
            HttpClient httpClient = HttpClientHelper.Create(url);
            string result = httpClient.GetString();
            var model = JsonSerializer.DeserializeFromString<StorageCheck>(result);
            ViewData["STORAGECHECK_ADD_OR_EDIT"] = "E";
            return View("StorageCheckForm", model);
        }

        /// <summary>
        /// 删除产品数据
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

            return RedirectToAction("Index", "StorageCheck");
        }
    }
}
