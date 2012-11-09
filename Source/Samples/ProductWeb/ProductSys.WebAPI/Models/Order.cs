using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductSys.WebAPI.Models
{
    public partial class Order
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public int BuyAmount { get; set; }
        public System.DateTime BuyDate { get; set; }
        public string BuyPerson { get; set; }
        public bool IsArrivaled { get; set; }
        public string Notes { get; set; }
        public Nullable<int> DiscountPercentage { get; set; }
        public Nullable<System.DateTime> ArrivaledDate { get; set; }
    }
}