using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductSys.DAL
{
    public class EPProduct
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public Nullable<int> UnitPrice { get; set; }
        public string Notes { get; set; }
    }
}