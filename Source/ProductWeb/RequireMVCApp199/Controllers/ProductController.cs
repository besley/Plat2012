using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Plat.WebUtility;
using ServiceStack.Text;
using ProductSys.BizEntity;

namespace CssStyleApp199.Controllers
{
    public class ProductController : WebControllerBase
    {
        public ProductController()
        {
            base.ApiUrl = "http://localhost:8081/ProductSys.WebAPI/api/Product";
        }

        public ActionResult List()
        {
            return View("ProductList");
        }

        public ActionResult Create()
        {
            ViewData["PRODUCT_ADD_OR_EDIT"] = "A";
            var model = new Product();

            return View("ProductForm", model);
        }

        /// <summary>
        /// 创建产品数据
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Product product)
        {
            HttpClient httpClient = HttpClientHelper.Create(base.ApiUrl);
            var jsonValue = JsonSerializer.SerializeToString<Product>(product);
            string result = httpClient.Insert(jsonValue);

            return RedirectToAction("Index", "Product");
        }

        /// <summary>
        /// 适用于表单单条记录编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(Product entity)
        {
            HttpClient httpClient = HttpClientHelper.Create(base.ApiUrl);
            IList<Product> entityList = new List<Product>();
            entityList.Add(entity);

            var jsonValue = JsonSerializer.SerializeToString<IList<Product>>(entityList);
            ViewData["U_Product_Rows"] = jsonValue;
            string result = httpClient.Update(jsonValue);

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            string url = base.ApiUrl + "/Get/" + id;
            HttpClient httpClient = HttpClientHelper.Create(url);
            string result = httpClient.GetString();
            var model = JsonSerializer.DeserializeFromString<Product>(result);
            ViewData["PRODUCT_ADD_OR_EDIT"] = "E";
            return View("ProductForm", model);
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
            string url = base.ApiUrl + "/Delete/" + id;
            HttpClient httpClient = HttpClientHelper.Create(url);
            string result = httpClient.Delete();

            return RedirectToAction("Index", "Product");
        }
    }
}