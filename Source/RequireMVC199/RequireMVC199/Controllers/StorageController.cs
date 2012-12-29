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
    public class StorageController : RequireJSController
    {
        public StorageController()
        {
            base.ApiUrl = "http://localhost:8081/ProductSys.WebAPI/api/Storage";
        }
             
        //
        // GET: /Storage/
        public ActionResult List()
        {
            return View("StorageList");
        }

        public ActionResult Create()
        {
            ViewData["STORAGE_ADD_OR_EDIT"] = "A";
            var model = new Storage();
            return View("StorageForm", model);
        }

        /// <summary>
        /// 创建库存数据
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Storage storage)
        {
            HttpClient httpClient = HttpClientHelper.Create(base.ApiUrl);
            var jsonValue = JsonSerializer.SerializeToString<Storage>(storage);
            string result = httpClient.Insert(jsonValue);

            return RedirectToAction("Index", "Storage");
        }

        /// <summary>
        /// 适用于表单单条记录编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(Storage entity)
        {
            HttpClient httpClient = HttpClientHelper.Create(base.ApiUrl);
            IList<Storage> entityList = new List<Storage>();
            entityList.Add(entity);

            var jsonValue = JsonSerializer.SerializeToString<IList<Storage>>(entityList);
            ViewData["U_Storage_Rows"] = jsonValue;
            string result = httpClient.Update(jsonValue);

            return RedirectToAction("Index", "Storage");
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            string url = base.ApiUrl + "/Get/" + id;
            HttpClient httpClient = HttpClientHelper.Create(url);
            string result = httpClient.GetString();
            var model = JsonSerializer.DeserializeFromString<Storage>(result);
            ViewData["STORAGE_ADD_OR_EDIT"] = "E";
            return View("StorageForm", model);
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

            return RedirectToAction("Index", "Storage");
        }
    }
}
