using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Plat.DataRepository;
using ProductSys.DAL;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace ProductSys.WebAPI.Models
{
    public class ProductRepository : RepositoryBase<EPProduct>, IProductRepository
    {
        protected override System.Data.Entity.DbContext CreateDbContext()
        {
            return DbContextCached.Current;
        }
    }
}


    