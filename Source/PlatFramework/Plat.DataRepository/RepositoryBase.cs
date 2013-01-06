using System;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;

namespace Plat.DataRepository
{
    /// <summary>
    /// Repository基类
    /// </summary>
    public class RepositoryBase : IDataRepository
    {
        private ISession _session;
        public ISession Session
        {
            get { return _session; }
        }

        public RepositoryBase(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        public T GetById<T>(dynamic primaryId) where T : class
        {
            return _session.Connection.Get<T>(primaryId as object);
        }

        /// <summary>
        /// 根据多个Id获取多个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public IEnumerable<T> GetByIds<T>(IList<dynamic> ids) where T:class
        {
            var tblName = string.Format("dbo.{0}", typeof(T).Name);
            var idsin = string.Join(",", ids.ToArray<dynamic>());
            var sql = "SELECT * FROM @table WHERE Id in (@ids)";
            IEnumerable<T> dataList = SqlMapper.Query<T>(_session.Connection, sql, new { table = tblName, ids = idsin });
            return dataList;
        }

        /// <summary>
        /// 获取全部数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>() where T:class
        {
            return _session.Connection.GetList<T>();
        }

        /// <summary>
        /// 根据条件筛选出数据集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<T> Get<T>(string sql, dynamic param = null, bool buffered = true) where T:class
        {
            return SqlMapper.Query<T>(_session.Connection, sql, param as object, _session.Transaction, buffered);
        }

        /// <summary>
        /// 根据条件筛选数据集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> Get(string sql, dynamic param = null, bool buffered = true)
        {
            return SqlMapper.Query(_session.Connection, sql, param as object, _session.Transaction, buffered);
        }

        /// <summary>
        /// 统计记录总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public int Count<T>(IPredicate predicate, bool buffered = false) where T : class
        {
            return _session.Connection.Count<T>(predicate);
        }

        /// <summary>
        /// 查询列表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(IPredicate predicate = null, IList<ISort> sort = null,
            bool buffered = false) where T : class
        {
            return _session.Connection.GetList<T>(predicate, sort, null, null, buffered);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="allRowsCount"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<T> GetPage<T>(int pageIndex, int pageSize, out long allRowsCount, 
            IPredicate predicate = null, ISort sort = null, bool buffered = true) where T : class
        {
            IList<ISort> orderBy = new List<ISort>();
            orderBy.Add(sort);

            return GetPage<T>(pageIndex, pageSize, out allRowsCount, predicate, orderBy, buffered);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="allRowsCount"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        public IEnumerable<T> GetPage<T>(int pageIndex, int pageSize, out long allRowsCount,
            IPredicate predicate, IList<ISort> sort, bool buffered = true) where T : class
        {
            IEnumerable<T> entityList = _session.Connection.GetPage<T>(predicate, sort, pageIndex, pageSize, null, null, buffered);
            allRowsCount = entityList.Count();

            return entityList;
        }

        /// <summary>
        /// 根据表达式筛选
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<TReturn> Get<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, 
            dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", 
            int? commandTimeout = null)
        {
            return SqlMapper.Query(_session.Connection, sql, map, param as object, transaction, buffered, splitOn);
        }

        /// <summary>
        /// 获取多实体集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public SqlMapper.GridReader GetMultiple(string sql, dynamic param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 执行sql操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int Execute(string sql, dynamic param = null, IDbTransaction transaction = null)
        {
            return _session.Connection.Execute(sql, param as object, transaction);
        }

        /// <summary>
        /// 插入单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public dynamic Insert<T>(T entity, IDbTransaction transaction=null) where T : class
        {
            dynamic result = _session.Connection.Insert<T>(entity, transaction);
            return result;
        }

        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update<T>(T entity, IDbTransaction transaction = null) where T : class
        {
            bool isOk = _session.Connection.Update<T>(entity, transaction);
            return isOk;
        }

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primaryId"></param>
        /// <returns></returns>
        public bool Delete<T>(dynamic primaryId, IDbTransaction transaction = null) where T : class
        {
            var entity = GetById<T>(primaryId);
            var obj = entity as T;
            bool isOk = _session.Connection.Delete<T>(obj);
            return isOk;
        }

        /// <summary>
        /// 删除单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Delete<T>(IPredicate predicate, IDbTransaction transaction = null) where T : class
        {
            return _session.Connection.Delete<T>(predicate, transaction);
        }

        /// <summary>
        /// 批量插入功能
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityList"></param>
        public void InsertBatch<T>(IEnumerable<T> entityList, IDbTransaction transaction = null) where T : class
        {
            //var tblName = string.Format("dbo.{0}", typeof(T).Name);
            //var conn = (SqlConnection)_session.Connection;
            //var tran = (SqlTransaction)transaction;
            //using (var bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock, tran))
            //{
            //    bulkCopy.BatchSize = entityList.Count();
            //    bulkCopy.DestinationTableName = tblName;
            //    var table = new DataTable();
            //    var props = TypeDescriptor.GetProperties(typeof(T))
            //                                .Cast<PropertyDescriptor>()
            //                                .Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System"))
            //                                .ToArray();
            //    foreach (var propertyInfo in props)
            //    {
            //        bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
            //        table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
            //    }
            //    var values = new object[props.Length];
            //    foreach (var itemm in entityList)
            //    {
            //        for (var i = 0; i < values.Length; i++)
            //        {
            //            values[i] = props[i].GetValue(itemm);
            //        }
            //        table.Rows.Add(values);
            //    }
            //    bulkCopy.WriteToServer(table);
            //}
        }

        /// <summary>
        /// 批量更新（）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public bool UpdateBatch<T>(IEnumerable<T> entityList, IDbTransaction transaction = null) where T : class
        {
            bool isOk = false;
            foreach (var item in entityList)
            {
                Update<T>(item, transaction);
            }
            isOk = true;
            return isOk;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteBatch<T>(IEnumerable<dynamic> ids, IDbTransaction transaction = null) where T : class
        {
            bool isOk = false;
            foreach (var id in ids)
            {
                Delete<T>(id, transaction);
            }
            isOk = true;
            return isOk;
        }
    }
}
