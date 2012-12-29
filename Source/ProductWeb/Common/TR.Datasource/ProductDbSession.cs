using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plat.DataRepository;

namespace TR.Datasource
{
    public class ProductDbSession : SessionBase, IProductDbSession
    {
        public ProductDbSession(IProductDatabase database)
            : base(database)
        {
        }
    }
}