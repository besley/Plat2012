using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plat.DataRepository;
using Plat.ServiceBuilder;
using ProductSys.BizEntity;

namespace ProductSys.Interface
{
    /// <summary>
    /// 产品服务接口
    /// </summary>
    public interface IProductService : IService
    {
        Product GetProductOrders(long productId);
        void UpdateBatch(IEnumerable<Product> entityList);
        object Find(long productId);
    }
}