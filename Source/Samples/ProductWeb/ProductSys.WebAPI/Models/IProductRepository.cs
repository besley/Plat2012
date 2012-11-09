using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plat.DataRepository;
using ProductSys.DAL;


namespace ProductSys.WebAPI.Models
{
    public interface IProductRepository
    {
        //Select
        EPProduct Get(object primaryID);
        IEnumerable<EPProduct> GetAll();
        IEnumerable<EPProduct> GetByIds(IList<object> ids);
        IEnumerable<EPProduct> Get(string where, params object[] values);
        IEnumerable<EPProduct> Get(int pageIndex, int pageSize, out long allRowsCount, string where, params object[] values);
        IEnumerable<EPProduct> Get(int pageIndex, int pageSize, out long allRowsCount, string orderByFieldName, string where, params object[] values);
        IEnumerable<EPProduct> Get(int pageIndex, int pageSize, out long allRowsCount, string orderByFieldName, SortType sortType, string where, params object[] values);
        IEnumerable<EPProduct> GetOrderByExpression(int pageIndex, int pageSize, out long allRowsCount, string orderByExpression, string where, params object[] values);

        //Insert, Update, Delete
        EPProduct Insert(EPProduct dataEntity);
        void Update(EPProduct dataEntity);
        void Delete(EPProduct dataEntity);
        void Delete(object primaryID);
    }
}