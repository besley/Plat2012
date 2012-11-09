using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AutoMapper;
using SimpleInjector;
using SimpleInjector.Extensions;
using SimpleInjector.Advanced;
using HttpContextShim.WebHost;
using Plat.DataRepository;
using Plat.ExceptionHelper;
using Plat.Logging;
using Plat.WebUtility;
using ProductSys.DAL;
using ProductSys.WebAPI.Models;

namespace ProductSys.WebAPI.Controllers
{
    public class ProductController : BaseController
    {
        protected IProductRepository ProductRepositoryInstance
        {
            get;
            private set;
        }

        public ProductController()
        {
            ProductRepositoryInstance = WebApiApplication.GlobalContainer.GetInstance<IProductRepository>();
        }

        // Get api/products
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            try
            {
                IEnumerable<EPProduct> pList = ProductRepositoryInstance.GetAll();
                AutoMapper.Mapper.CreateMap<EPProduct, Product>();
                return AutoMapper.Mapper.Map<IEnumerable<EPProduct>, IEnumerable<Product>>(pList);
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("获取产品信息发生异常！", 1002);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
                //throw new HttpResponseException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
            }
        }

        // GET api/product/5
        [HttpGet]
        public Product Get(string id)
        {
            try
            {
                EPProduct p = ProductRepositoryInstance.Get(long.Parse(id));
                AutoMapper.Mapper.CreateMap<EPProduct, Product>();
                //throw new ApplicationException("this is test error message");
                if (p == null)
                {
                    //throw new HttpResponseException(HttpStatusCode.NotFound);
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                    { Content = new StringContent("id3 not found"), ReasonPhrase = "product id not exist." });
                }
                return AutoMapper.Mapper.Map<EPProduct, Product>(p);
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                
                var errorHandler = ErrorHandlerFactory.Create("获取产品信息发生异常！", 1002);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                //throw new WebFaultException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
                //throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                //{ Content = new StringContent("id3 not found"), ReasonPhrase = "product id not exist." });
                //throw new ApplicationException("id3 does not exist in db.");
                if (ex.InnerException != null)
                    throw ex.InnerException;
                else
                    throw ex;
                //return null;
            }
        }

        [HttpGet]
        public HttpResponseMessage Getdetails([FromBody] string value)
        {
            var response = Request.CreateResponse<string>(HttpStatusCode.Found, value);
            System.Diagnostics.Debug.WriteLine(value);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage PostSomething([FromBody] string value)
        {
            var response = Request.CreateResponse<string>(HttpStatusCode.Created, value);
            System.Diagnostics.Debug.WriteLine(value);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage Insert(Product entity)
        {
            try
            {
                AutoMapper.Mapper.CreateMap<Product, EPProduct>();
                EPProduct p = AutoMapper.Mapper.Map<Product, EPProduct>(entity);
                ProductRepositoryInstance.Insert(p);

                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, entity);
                string uri = Url.Link("product", new { id = p.ID });
                response.Headers.Location = new Uri(uri);
                return response;

            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("插入产品信息发生异常！", 1003);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                //throw new WebFaultException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
                throw ex;
            }
        }

        [HttpPut]
        public HttpResponseMessage Update(Product entity)
        {
            try
            {
                AutoMapper.Mapper.CreateMap<Product, EPProduct>();
                EPProduct p = AutoMapper.Mapper.Map<Product, EPProduct>(entity);
                ProductRepositoryInstance.Update(p);

                var response = Request.CreateResponse<Product>(HttpStatusCode.Accepted, entity);
                string uri = Url.Link("product", new { id = p.ID });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("更新产品信息发生异常！", 1004);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                //throw new WebFaultException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
                throw ex;
            }
        }

        //public void Delete(string id)
        //{
        //    try
        //    {
        //        ProductRepositoryInstance.Delete(id);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //记录异常日志信息
        //        var errorHandler = ErrorHandlerFactory.Create("删除产品信息发生异常！", 1005);
        //        LogHelper.Error(errorHandler.GetInfo(), ex);
        //        //throw new WebFaultException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
        //    }
        //}
    }
}
