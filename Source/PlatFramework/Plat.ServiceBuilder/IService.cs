using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using Plat.ServiceBuilder.Injection;
using Plat.DataRepository;

namespace Plat.ServiceBuilder
{
    /// <summary>
    /// 服务接口类
    /// </summary>
    public interface IService
    {
        IContainer Container { get; set; }
        ISession Session { get; set; }
        IDataRepository DataRepository { get; set; }
        void Initialize(IContainer container, ISession session);

        //select
        T1 GetById<T1, T2>(dynamic primaryId)
            where T1 : class
            where T2 : class;

        IEnumerable<T1> GetByIds<T1, T2>(IList<dynamic> ids)
            where T1 : class
            where T2 : class;

        IEnumerable<T1> GetAll<T1, T2>()
            where T1 : class
            where T2 : class;

        IEnumerable<T1> Get<T1, T2>(string sql, dynamic param = null)
            where T1 : class
            where T2 : class;

        IEnumerable<TReturn> Get<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map,
            dynamic param = null);

        //paged select
        IEnumerable<T1> GetPage<T1, T2>(int pageIndex, int pageSize, out long allRowsCount, IPredicate predicate = null, ISort sort = null)
            where T1 : class
            where T2 : class;

        IEnumerable<T1> GetPage<T1, T2>(int pageIndex, int pageSize, out long allRowsCount, IPredicate predicate = null, IList<ISort> sort = null)
            where T1 : class
            where T2 : class;

        //execute
        Int32 Execute<T1, T2>(string sql, dynamic param = null, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class;

        //insert, update, delete
        dynamic Insert<T1, T2>(T1 entity, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class;

        void InsertBatch<T1, T2>(IEnumerable<T1> entityList, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class;

        bool Update<T1, T2>(T1 entity, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class;

        bool UpdateBatch<T1, T2>(IEnumerable<T1> entityList, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class;

        bool Delete<T1, T2>(dynamic primaryId, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class;

        bool Delete<T1, T2>(IPredicate predicate, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class;

        bool DeleteBatch<T1, T2>(IEnumerable<dynamic> ids, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class;
    }
}
