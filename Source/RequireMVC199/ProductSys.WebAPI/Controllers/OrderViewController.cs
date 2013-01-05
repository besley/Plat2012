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
using Plat.ServiceBuilder;
using Plat.Logging;
using ProductSys.DAL;
using ProductSys.BizEntity;
using ProductSys.Interface;
using ProductSys.WebAPI.Core;

namespace ProductSys.WebAPI.Controllers
{
    public class OrderViewController : ApiControllerBase
    {
        protected IOrderService OrderServiceInstance
        {
            get;
            private set;
        }

        public OrderViewController()
        {
            OrderServiceInstance = ServiceManager<IOrderService>.CreateService("IOrderService");
        }

        [HttpGet]
        public OrderView Get(string id)
        {
            var orderView = OrderServiceInstance.GetOrderView(id);
            return orderView;
        }

        // GET api/orders
        /// <summary>
        /// 按查询条件获取订单视图
        /// 客户端调用示例：
        /// [/ProductSys.WebAPI/api/OrderView?ProductId=2]
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<OrderView> Get([FromUri] OrderQuery query)
        {
            try
            {
                IEnumerable<Order> dataList;
                if (query != null && query.ProductId > 0)
                    dataList = OrderServiceInstance.Get<Order, EPOrder>("SELECT * FROM EPORDER WHERE ProductId=@productId", new { ProductId = query.ProductId });
                else
                    dataList = OrderServiceInstance.GetAll<Order, EPOrder>();

                IEnumerable<OrderView> entityList = dataList.Select(Mapper.DynamicMap<OrderView>);
                return entityList;
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("获取订单信息发生异常！", 1002);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }
        }

        [HttpGet]
        public void AppendOrderAmount(string id)
        {
            OrderServiceInstance.AppendOrderAmount(id);
        }

        [HttpPost]
        public HttpResponseMessage Insert(OrderView entity)
        {
            try
            {
                AutoMapper.Mapper.CreateMap<OrderView, Order>();
                Order p = AutoMapper.Mapper.Map<OrderView, Order>(entity);
                OrderServiceInstance.Insert<Order, EPOrder>(p);

                var response = Request.CreateResponse<OrderView>(HttpStatusCode.Created, entity, "application/json");
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
        public HttpResponseMessage Update(IEnumerable<OrderView> entityList)
        {
            try
            {
                OrderServiceInstance.UpdateBatch(entityList);

                var response = Request.CreateResponse<string>(HttpStatusCode.Created, 
                    "更新产品数据成功！", 
                    "application/json");
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

        [HttpDelete]
        public HttpResponseMessage Delete(string id)
        {
            try
            {
                OrderServiceInstance.Delete<Order, EPOrder>(long.Parse(id));

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
