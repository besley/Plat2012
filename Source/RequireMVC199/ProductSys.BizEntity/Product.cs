using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductSys.BizEntity
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public Nullable<int> UnitPrice { get; set; }
        public string Notes { get; set; }

        /// <summary>
        /// 产品所包含的订单数据列表
        /// </summary>
        public IEnumerable<Order> OrderList { get; set; }
    }
}