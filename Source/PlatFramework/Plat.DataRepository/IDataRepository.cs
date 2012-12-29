using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;

namespace Plat.DataRepository
{
    /// <summary>
    /// Data Repository
    /// Implement Select, Insert, Update, Delete
    /// </summary>
    public interface IDataRepository
    {
        ISession Session { get; }

        //select
        T GetById<T>(dynamic primaryId) where T : class;
        IEnumerable<T> GetByIds<T>(IList<dynamic> ids) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        IEnumerable<T> Get<T>(string sql, dynamic param = null, bool buffered = true) where T : class;
        IEnumerable<dynamic> Get(string sql, dynamic param = null, bool buffered = true);
        IEnumerable<TReturn> Get<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map,
            dynamic param = null, IDbTransaction transaction = null, bool buffered = true,
            string splitOn = "Id", int? commandTimeout = null);
        SqlMapper.GridReader GetMultiple(string sql, dynamic param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

        //count
        int Count<T>(IPredicate predicate, bool buffered = false) where T : class;

        //lsit
        IEnumerable<T> GetList<T>(IPredicate predicate = null, IList<ISort> sort = null, bool buffered = false) where T : class;

        //paged select
        IEnumerable<T> GetPage<T>(int pageIndex, int pageSize, out long allRowsCount, IPredicate predicate = null, ISort sort = null, bool buffered = true) where T : class;
        IEnumerable<T> GetPage<T>(int pageIndex, int pageSize, out long allRowsCount, IPredicate predicate = null, IList<ISort> sort = null, bool buffered = true) where T : class;

        //execute
        Int32 Execute(string sql, dynamic param = null, IDbTransaction transaction = null);
 

        //insert, update, delete
        dynamic Insert<T>(T entity, IDbTransaction transaction=null) where T : class;
        void InsertBatch<T>(IEnumerable<T> entityList, IDbTransaction transaction=null) where T : class;
        bool Update<T>(T entity, IDbTransaction transaction=null) where T : class;
        bool UpdateBatch<T>(IEnumerable<T> entityList, IDbTransaction transaction=null) where T : class;
        bool Delete<T>(dynamic primaryId, IDbTransaction transaction=null) where T : class;
        bool Delete<T>(IPredicate predicate, IDbTransaction transaction=null) where T : class;
        bool DeleteBatch<T>(IEnumerable<dynamic> ids, IDbTransaction transaction=null) where T : class;
    }
}
