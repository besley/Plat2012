using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plat.DataRepository;

namespace TR.Datasource
{
    public class ProductDatabase : Database, IProductDatabase
    {
        public ProductDatabase(IDbConnection connection)
            : base(connection)
        {

        }
    }
}