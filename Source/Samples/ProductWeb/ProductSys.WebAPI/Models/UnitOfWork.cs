using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plat.DataRepository;
using ProductSys.DAL;

namespace ProductSys.WebAPI.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        public void SaveChanges()
        {
            ProductDbContext dbContext = DbContextCached.Current;
            if (dbContext != null)
                dbContext.SaveChanges();
        }
    }
}