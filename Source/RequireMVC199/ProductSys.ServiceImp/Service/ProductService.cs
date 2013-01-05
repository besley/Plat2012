using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using Plat.WebUtility;
using Plat.DataRepository;
using Plat.ServiceBuilder;
using ProductSys.DAL;
using ProductSys.BizEntity;
using ProductSys.Interface;

namespace ProductSys.ServiceImp
{
    public partial class ProductService : ServiceBase, IProductService
    {
        public object Find(long productId)
        {
            return "hello";
        }

        public void UpdateBatch(IEnumerable<Product> entityList)
        {
            foreach (var entity in entityList)
            {
                Update<Product, EPProduct>(entity);
            }
        }

        /// <summary>
        /// 获取产品及产品的订单数据
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product GetProductOrders(long productId)
        {
            Product product = GetById<Product, EPProduct>(productId);

            IOrderService orderService = base.Container.GetInstance<IOrderService>();
            orderService.Initialize(base.Container, base.Session);

            product.OrderList = orderService.Get<Order, EPOrder>("SELECT * FROM EPORDER WHERE ProductId=@productId", new { productId = productId });

            return product;
        }
    }
}


    