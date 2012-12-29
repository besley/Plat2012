using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using AutoMapper;
using Dapper;
using DapperExtensions;
using Plat.DataRepository;
using Plat.ExceptionHelper;
using Plat.Logging;
using Plat.WebUtility;
using Plat.ServiceBuilder;
using ProductSys.BizEntity;
using ProductSys.BizEntity.Queryable;
using ProductSys.Interface;
using ProductSys.WebAPI.Core;
using ProductSys.DAL;

namespace ProductSys.WebAPI.Controllers
{
    public class ProductController : ApiControllerBase
    {
        protected IProductService ProductServiceInstance
        {
            get;
            private set;
        }

        public ProductController()
        {
            ProductServiceInstance = ServiceManager<IProductService>.CreateService("IProductService");
        }

        [HttpGet]
        public object Find(string id)
        {
            return ProductServiceInstance.Find(2);
        }

        // Get api/products
        [HttpGet]
        public IEnumerable<Product> Get([FromUri]ProductQuery query)
        {
            try
            {
                IEnumerable<Product> pList;
                if (query != null && query.ProductType != null)
                {
                    pList = ProductServiceInstance.Get<Product, EPProduct>("SELECT * FROM EPProduct WHERE ProductType=@productTypeId", new { productTypeId = query.ProductType })
                        .ToList<Product>(); ;
                }
                else
                {
                    pList = ProductServiceInstance.GetAll<Product, EPProduct>();
                }

                return pList;
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
                var p = ProductServiceInstance.GetById<Product, EPProduct>(long.Parse(id));
                //throw new ApplicationException("this is test error message");
                if (p == null)
                {
                    //throw new HttpResponseException(HttpStatusCode.NotFound);
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                    { Content = new StringContent("id3 not found"), ReasonPhrase = "product id not exist." });
                }
                return p;
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                
                var errorHandler = ErrorHandlerFactory.Create("获取产品信息发生异常！", 1002);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                if (ex.InnerException != null)
                    throw ex.InnerException;
                else
                    throw ex;
            }
        }

        [HttpGet]
        public IEnumerable<Product> GetPaged()
        {
            long allRowsCount = 0;
            ISort sort = new Sort();
            sort.PropertyName = "ID";
            sort.Ascending = false;

            //多条件分页查询
            var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            pg.Predicates.Add(Predicates.Field<EPProduct>(f => f.ProductName, Operator.Like, "%c%"));
            pg.Predicates.Add(Predicates.Field<EPProduct>(f => f.ProductType, Operator.Gt, 3));

            IEnumerable<Product> productList = ProductServiceInstance.GetPage<Product, EPProduct>(0, 10, out allRowsCount, pg, sort).ToList<Product>();
            return productList;
        }

        [HttpGet]
        public Product GetProductOrders(string id)
        {
            try
            {
                var p = ProductServiceInstance.GetProductOrders(long.Parse(id));
                if (p == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("id3 not found"), ReasonPhrase = "product id not exist." });
                }
                return p;
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("获取产品信息发生异常！", 1002);
                LogHelper.Error(errorHandler.GetInfo(), ex);

                if (ex.InnerException != null)
                    throw ex.InnerException;
                else
                    throw ex;
            }
        }

        [HttpPost]
        public HttpResponseMessage Create(Product entity)
        {
            try
            {
                ProductServiceInstance.Insert<Product, EPProduct>(entity);

                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, entity);
                //string uri = Url.Link("product", new { id = p.ID });
                //response.Headers.Location = new Uri(uri);
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
                ProductServiceInstance.Update<Product, EPProduct>(entity);

                var response = Request.CreateResponse<string>(HttpStatusCode.Created,
                    "更新产品数据成功！",
                    "application/json");
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

        [HttpPut]
        public HttpResponseMessage UpdateBatch(IEnumerable<Product> entityList)
        {
            try
            {
                ProductServiceInstance.UpdateBatch(entityList);
                
                var response = Request.CreateResponse<string>(HttpStatusCode.Created,
                    "更新产品数据成功！",
                    "application/json");
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

        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {
            try
            {
                ProductServiceInstance.Delete<Product, EPProduct>(id);

                var response = Request.CreateResponse<string>(HttpStatusCode.Created, "删除成功！", "application/json");
                return response;
               
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("删除产品信息发生异常！", 1005);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                //throw new WebFaultException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("删除失败！"),
                    ReasonPhrase = ex.Message
                });
            }
        }
    }
}