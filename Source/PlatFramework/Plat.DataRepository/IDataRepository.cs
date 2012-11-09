using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace Plat.DataRepository
{
    /// <summary>
    /// 数据持久化操作的接口
    /// </summary>
    /// <typeparam name="TDataEntity"></typeparam>
    public interface IDataRepository<TDataEntity> : IDisposable 
        where TDataEntity : class
    {
        DbContext DbContext { get; }
        DbSet<TDataEntity> DbSet { get; }
        IList<object> IDs { get; }
        Action<IList<object>> FlashCache { get; }
                    
        //Select
        TDataEntity Get(object primaryID);
        IEnumerable<TDataEntity> GetAll();
        IEnumerable<TDataEntity> GetByIds(IList<object> ids);
        IEnumerable<TDataEntity> Get(string where, params object[] values);
        IEnumerable<TDataEntity> Get(int pageIndex, int pageSize, out long allRowsCount, string where, params object[] values);
        IEnumerable<TDataEntity> Get(int pageIndex, int pageSize, out long allRowsCount, string orderByFieldName, string where, params object[] values);
        IEnumerable<TDataEntity> Get(int pageIndex, int pageSize, out long allRowsCount, string orderByFieldName, SortType sortType, string where, params object[] values);
        IEnumerable<TDataEntity> GetOrderByExpression(int pageIndex, int pageSize, out long allRowsCount, string orderByExpression, string where, params object[] values);

        //Insert, Update, Delete
        TDataEntity Insert(TDataEntity dataEntity);
        void Update(TDataEntity dataEntity);
        void Delete(TDataEntity dataEntity);
        void Delete(object primaryID);
        
        //SaveChanges
        void SaveChanges();
    }
}
