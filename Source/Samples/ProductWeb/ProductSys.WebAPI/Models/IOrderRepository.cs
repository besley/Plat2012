using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plat.DataRepository;
using ProductSys.DAL;

namespace ProductSys.WebAPI.Models
{
    public interface IOrderRepository
    {
        //Select
        EPOrder Get(object primaryID);
        IEnumerable<EPOrder> GetAll();
        IEnumerable<EPOrder> GetByIds(IList<object> ids);
        IEnumerable<EPOrder> Get(string where, params object[] values);
        IEnumerable<EPOrder> Get(int pageIndex, int pageSize, out long allRowsCount, string where, params object[] values);
        IEnumerable<EPOrder> Get(int pageIndex, int pageSize, out long allRowsCount, string orderByFieldName, string where, params object[] values);
        IEnumerable<EPOrder> Get(int pageIndex, int pageSize, out long allRowsCount, string orderByFieldName, SortType sortType, string where, params object[] values);
        IEnumerable<EPOrder> GetOrderByExpression(int pageIndex, int pageSize, out long allRowsCount, string orderByExpression, string where, params object[] values);

        //Insert, Update, Delete
        EPOrder Insert(EPOrder dataEntity);
        void Update(EPOrder dataEntity);
        void Delete(EPOrder dataEntity);
        void Delete(object primaryID);
    }
}