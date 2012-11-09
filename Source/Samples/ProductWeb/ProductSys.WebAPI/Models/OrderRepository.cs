using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plat.DataRepository;
using ProductSys.DAL;

namespace ProductSys.WebAPI.Models
{
    public class OrderRepository : RepositoryBase<EPOrder>, IOrderRepository
    {
        protected override System.Data.Entity.DbContext CreateDbContext()
        {
            return DbContextCached.Current;
        }
    }
}