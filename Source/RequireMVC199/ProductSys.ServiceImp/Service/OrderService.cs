using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Dapper;
using DapperExtensions;
using Plat.WebUtility;
using Plat.DataRepository;
using Plat.ServiceBuilder;
using ProductSys.BizEntity;
using ProductSys.DAL;
using ProductSys.Interface;
using System.Dynamic;


namespace ProductSys.ServiceImp
{
    /// <summary>
    /// 订单服务
    /// </summary>
    public partial class OrderService : ServiceBase, IOrderService
    {
        /// <summary>
        /// 订单视图类型数据
        /// 多表复杂视图通过db实现如存储过程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderView GetOrderView(string id)
        {
            var entityList = base.DataRepository.Get(@"SELECT A.ID, A.ProductId, B.ProductName, A.BuyAmount,
                                                    A.BuyDate,A.BuyPerson,A.IsArrivaled,A.Notes,A.DiscountPercentage,A.ArrivaledDate
                                                  FROM dbo.EPOrder A INNER JOIN dbo.EPProduct B ON A.ProductId = B.Id
                                                  WHERE A.ID = @Id", new {ID= id});
            var entity = entityList.FirstOrDefault<dynamic>();
            var orderView = AutoMapperHelper<dynamic, OrderView>.AutoConvertDynamic(entity);

            return orderView;

        }

        public void AppendOrderAmount(string id)
        {
            var order = base.DataRepository.GetById<EPOrder>(id) as EPOrder;
            order.BuyAmount += 1;

            var product = base.DataRepository.GetById<EPProduct>(order.ProductId) as EPProduct;
            product.Notes += "order count increasing 1";

            var transaction = base.Session.Begin();
            try
            {
                base.DataRepository.Update<EPOrder>(order, transaction);
                base.DataRepository.Update<EPProduct>(product, transaction);
                base.Session.Commit();
            }
            catch (System.Exception ex)
            {
                base.Session.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 插入订单，并更新产品记录
        /// </summary>
        /// <param name="order"></param>
        public void InsertWith(Order order)
        {
            //插入订单
            this.Insert<Order, EPOrder>(order);
            
            //更新产品主记录的订单数
            IProductService productService = base.Container.GetInstance<IProductService>();
            productService.Initialize(base.Container, base.Session);
            Product product = productService.GetById<Product, EPProduct>(order.ProductId);

            //暂时注释，用以表述多事务提交
            //product.OrderCount = (product.OrderCount == null) ? 1: product.OrderCount + 1;
            productService.Update<Product, EPProduct>(product);
        }

        /// <summary>
        /// 批量更新订单数据
        /// </summary>
        /// <param name="entityList"></param>
        public void UpdateBatch(IEnumerable<OrderView> entityList)
        {
            Order epOrder = null;
            foreach (var entity in entityList)
            {
                epOrder = AutoMapperHelper<OrderView, Order>.AutoConvert(entity);
                Update<Order, EPOrder>(epOrder);
            }
        }

        /// <summary>
        /// 订单视图列表数据
        /// 多表复杂视图通过db实现如存储过程
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetByQuery(OrderQuery query)
        {
            IEnumerable<Order> list = base.DataRepository.Get<Order>("SELECT * FROM EPORDER WHERE ProductId=@productId", new {ProductId = query.ProductId});

            return list;
        }

        public string TestMessage(string message)
        {
            return "England " + message;
        }
    }
}