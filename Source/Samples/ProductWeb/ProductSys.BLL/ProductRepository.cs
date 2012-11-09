using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductSys.DAL;
using Plat.DataRepository;

namespace ProductSys.BLL
{
    public class ProductRepository : RepositoryBase<EPProduct>
    {
        public override System.Data.Entity.DbContext GetEntityDbContext()
        {
            if (base.DbContext == null)
                return new ProductDbContext();
            else
                return base.DbContext;
        }
    }
}
