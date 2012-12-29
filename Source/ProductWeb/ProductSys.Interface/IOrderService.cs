using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.Composition;
using Plat.DataRepository;
using Plat.ServiceBuilder;
using ProductSys.BizEntity;

namespace ProductSys.Interface
{
    [InheritedExport(typeof(IOrderService))]
    public interface IOrderService: IService
    {
        /// <summary>
        /// 获取订单视图数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OrderView GetOrderView(string id);

        /// <summary>
        /// 插入订单，并更新关联数据
        /// </summary>
        /// <param name="order"></param>
        void InsertWith(Order order);

        /// <summary>
        /// 批量更新订单
        /// </summary>
        /// <param name="entityList"></param>
        void UpdateBatch(IEnumerable<OrderView> entityList);

        /// <summary>
        /// 测试消息方法
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        string TestMessage(string message);

        void AppendOrderAmount(string id);

    }
}