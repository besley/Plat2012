using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductSys.WebAPI.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public Nullable<short> UnitPrice { get; set; }
        public Nullable<int> OrderCount { get; set; }
    }
}