using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using Plat.DataRepository;
using Plat.ServiceBuilder;
using ProductSys.Interface;
using ProductSys.BizEntity;
using ProductSys.DAL;

namespace ProductSys.PlugIn.Service
{
    public class OrderService: ServiceBase, IOrderService
    {
        public OrderView GetOrderView(string id)
        {
            throw new NotImplementedException();
        }

        public void InsertWith(Order order)
        {
            throw new NotImplementedException();
        }

        public void UpdateBatch(IEnumerable<OrderView> entityList)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderView> GetByQuery(OrderQuery query)
        {
            throw new NotImplementedException();
        }


        public string TestMessage(string message)
        {
            return "Hello Plugin: " + message;
        }

        public IEnumerable<object> GetOrderListTest(long productId)
        {
            throw new NotImplementedException();
        }

        public object GetOrderTest(long orderId)
        {
            throw new NotImplementedException();
        }


        public void AppendOrderAmount(string id)
        {
            throw new NotImplementedException();
        }
    }
}
