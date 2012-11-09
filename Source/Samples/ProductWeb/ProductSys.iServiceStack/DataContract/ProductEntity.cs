using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceModel;
using ServiceStack.ServiceHost;

namespace ProductSys.iServiceStack.DataContract
{
    [DataContract]
    [Description("Product Entity in Service Layer.")]
    [Route("/Product")]
    [Route("/Product/{id}")]
    [Route("/Product/{name}")]
    public class Product
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public Nullable<short> UnitPrice { get; set; }
    }

    [DataContract]
    public class ProductResponse
    {
        [DataMember]
        public string Result { get; set; }
    }
}

