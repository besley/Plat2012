using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ProductSys.Service.DataContract
{
    [DataContract]
    public partial class Product
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
}
