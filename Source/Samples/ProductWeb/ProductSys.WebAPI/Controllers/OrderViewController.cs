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
    public class OrderViewController : BaseController
    {
        protected IOrderRepository OrderRepositoryInstance
        {
            get;
            private set;
        }

        public OrderViewController()
        {
            OrderRepositoryInstance = WebApiApplication.GlobalContainer.GetInstance<IOrderRepository>();
        }

        // GET api/orders
        [HttpGet]
        public IEnumerable<OrderView> Get()
        {
            try
            {
                var orderList =
                    from order in DbContextCached.Current.EPOrder
                    join product in DbContextCached.Current.EPProduct
                    on order.ProductId equals product.ID
                    select new OrderView
                    {
                        ID = order.ID,
                        ProductId = order.ProductId,
                        ProductName = product.ProductName,
                        BuyAmount = order.BuyAmount,
                        BuyDate = order.BuyDate,
                        BuyPerson = order.BuyPerson,
                        IsArrivaled = order.IsArrivaled,
                        Notes = order.Notes,
                        DiscountPercentage = order.DiscountPercentage
                    };

                return orderList;
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

        //[HttpPut]
        //public HttpResponseMessage Update(OrderView entity)
        //{
        //    try
        //    {
        //        AutoMapper.Mapper.CreateMap<OrderView, EPOrder>();
        //        EPOrder p = AutoMapper.Mapper.Map<OrderView, EPOrder>(entity);
        //        OrderRepositoryInstance.Update(p);

        //        var response = Request.CreateResponse<OrderView>(HttpStatusCode.Created, 
        //            entity, 
        //            "application/json");
        //        return response;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        //记录异常日志信息
        //        var errorHandler = ErrorHandlerFactory.Create("插入产品信息发生异常！", 1003);
        //        LogHelper.Error(errorHandler.GetInfo(), ex);
        //        //throw new WebFaultException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
        //        throw ex;
        //    }
        //}

        [HttpPut]
        public HttpResponseMessage Update(IList<OrderView> entityList)
        {
            try
            {
                foreach (var entity in entityList)
                {
                    AutoMapper.Mapper.CreateMap<OrderView, EPOrder>();
                    EPOrder p = AutoMapper.Mapper.Map<OrderView, EPOrder>(entity);
                    OrderRepositoryInstance.Update(p);
                }

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

    }
}
