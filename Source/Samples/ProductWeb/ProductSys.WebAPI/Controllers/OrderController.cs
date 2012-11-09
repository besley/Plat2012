using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Plat.ExceptionHelper;
using Plat.DataRepository;
using Plat.WebUtility;
using Plat.Logging;
using ProductSys.DAL;
using ProductSys.WebAPI.Models;

namespace ProductSys.WebAPI.Controllers
{
    public class OrderController : BaseController
    {
        protected IOrderRepository OrderRepositoryInstance
        {
            get;
            private set;
        }

        public OrderController()
        {
            OrderRepositoryInstance = WebApiApplication.GlobalContainer.GetInstance<IOrderRepository>();
        }

        // GET api/orders
        [HttpGet]
        public IEnumerable<Order> GetOrders()
        {
            try
            {
                IEnumerable<EPOrder> oList = OrderRepositoryInstance.GetAll();
                AutoMapper.Mapper.CreateMap<EPOrder, Order>();
                return AutoMapper.Mapper.Map<IEnumerable<EPOrder>, IEnumerable<Order>>(oList);
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
                EPOrder p = OrderRepositoryInstance.Get(long.Parse(id));
                AutoMapper.Mapper.CreateMap<EPOrder, Order>();
                //throw new ApplicationException("this is test error message");
                if (p == null)
                {
                    //throw new HttpResponseException(HttpStatusCode.NotFound);
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { 
                        Content = new StringContent("id3 not found"), 
                        ReasonPhrase = "product id not exist." });
                }

                return AutoMapper.Mapper.Map<EPOrder, Order>(p);
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

        [HttpPost]
        public HttpResponseMessage Insert(Order entity)
        {
            try
            {
                AutoMapper.Mapper.CreateMap<Order, EPOrder>();
                EPOrder p = AutoMapper.Mapper.Map<Order, EPOrder>(entity);
                OrderRepositoryInstance.Insert(p);

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
                AutoMapper.Mapper.CreateMap<Order, EPOrder>();
                EPOrder order = AutoMapper.Mapper.Map<Order, EPOrder>(entity);
                OrderRepositoryInstance.Insert(order);

                IProductRepository pRepo = WebApiApplication.GlobalContainer.GetInstance<IProductRepository>();
                //IProductRepository pRepo = new ProductRepository();
                EPProduct product = pRepo.Get(order.ProductId);
                product.OrderCount = (product.OrderCount == null) ? 1: product.OrderCount + 1;
                pRepo.Update(product);

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
                AutoMapper.Mapper.CreateMap<Order, EPOrder>();
                EPOrder p = AutoMapper.Mapper.Map<Order, EPOrder>(entity);
                OrderRepositoryInstance.Update(p);

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
                OrderRepositoryInstance.Delete(long.Parse(id));
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
