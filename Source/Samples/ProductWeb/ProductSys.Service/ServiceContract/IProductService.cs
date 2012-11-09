using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using ProductSys.Service.DataContract;

namespace ProductSys.Service.ServiceContract
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        [WebGet(UriTemplate="Products")]
        IEnumerable<Product> GetProducts();

        [OperationContract]
        [WebGet(UriTemplate="Product/{id}")]
        Product Get(string id);

        [OperationContract]
        [WebInvoke(Method="POST", UriTemplate="Product")]
        void Insert(Product entity);

        [OperationContract]
        [WebInvoke(Method="PUT", UriTemplate="Product")]
        void Update(Product entity);

        [OperationContract]
        [WebInvoke(Method="DELETE", UriTemplate="Product/{id}")]
        void Delete(string id);
    }
}
