using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Dapper;
using DapperExtensions;
using Plat.ExceptionHelper;
using Plat.DataRepository;
using Plat.WebUtility;
using Plat.Logging;
using Plat.ServiceBuilder;
using ProductSys.DAL;
using ProductSys.BizEntity;
using ProductSys.Interface;
using ProductSys.WebAPI.Core;

namespace ProductSys.WebAPI.Controllers
{
    public class OrderController : ApiControllerBase
    {
        protected IOrderService OrderServiceInstance
        {
            get;
            private set;
        }

        public OrderController()
        {
            OrderServiceInstance = ServiceManager<IOrderService>.CreateService("IOrderService");
        }

        // GET api/orders
        [HttpGet]
        public string GetMessage()
        {
            try
            {
                string s = OrderServiceInstance.TestMessage("Moon");
                return s;
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("获取订单信息发生异常！", 1002);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
                //throw new HttpResponseException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
            }
        }


        // GET api/orders
        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            try
            {
                IEnumerable<Order> oList = OrderServiceInstance.GetAll<Order, EPOrder>();
                return oList;
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("获取订单信息发生异常！", 1002);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
                //throw new HttpResponseException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
            }
        }

        // GET api/order/5
        [HttpGet]
        public Order Get(string id)
        {
            try
            {
                Order p = OrderServiceInstance.GetById<Order, EPOrder>(long.Parse(id));
                //throw new ApplicationException("this is test error message");
                if (p == null)
                {
                    //throw new HttpResponseException(HttpStatusCode.NotFound);
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { 
                        Content = new StringContent("id3 not found"), 
                        ReasonPhrase = "product id not exist." });
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
                //return null;
            }
        }

        [HttpPost]
        public HttpResponseMessage Insert(Order entity)
        {
            try
            {
                OrderServiceInstance.Insert<Order, EPOrder>(entity);

                var response = Request.CreateResponse<Order>(HttpStatusCode.Created, entity, "application/json");
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

        [HttpPost]
        public HttpResponseMessage InsertWith(Order entity, string type)
        {
            try
            {
                OrderServiceInstance.InsertWith(entity);

                var response = Request.CreateResponse<Order>(HttpStatusCode.Created, entity, "application/json");
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
        public HttpResponseMessage Update(Order entity)
        {
            try
            {
                OrderServiceInstance.Update<Order, EPOrder>(entity);

                var response = Request.CreateResponse<Order>(HttpStatusCode.Accepted, entity);
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
                OrderServiceInstance.Delete<Order, EPOrder>(long.Parse(id));
                var response = Request.CreateResponse<Order>(HttpStatusCode.Found, null, "application/json");
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
