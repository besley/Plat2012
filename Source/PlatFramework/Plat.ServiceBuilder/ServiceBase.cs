using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using AutoMapper;
using Plat.WebUtility;
using Plat.DataRepository;
using Plat.ServiceBuilder.Injection;

namespace Plat.ServiceBuilder
{
    /// <summary>
    /// 服务基类
    /// </summary>
    /// <typeparam name="T1">业务实体类</typeparam>
    /// <typeparam name="T2">数据实体类</typeparam>
    public abstract class ServiceBase : IService
    {
        private IContainer _container;
        private ISession _session;
        private IDataRepository _dataRepository;

        /// <summary>
        /// Ioc容器对象
        /// </summary>
        public IContainer Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        /// <summary>
        /// 数据库Session对象
        /// </summary>
        public ISession Session
        {
            get
            {
                return _session;
            }
            set
            {
                _session = value;
            }
        }

        /// <summary>
        /// 数据操作对象
        /// </summary>
        public IDataRepository DataRepository
        {
            get
            {
                return _dataRepository;
            }
            set
            {
                _dataRepository = value;
            }
        }

        /// <summary>
        /// 服务初始化操作
        /// </summary>
        /// <param name="container"></param>
        /// <param name="session"></param>
        public void Initialize(IContainer container, ISession session)
        {
            _container = container;
            _session = session;

            _dataRepository = new RepositoryBase(_session);
        }

        /// <summary>
        /// 按Id获取
        /// </summary>
        /// <param name="primaryId">主键Id</param>
        /// <returns>业务对象</returns>
        public T1 GetById<T1, T2>(dynamic primaryId)
            where T1:class
            where T2:class
        {
            T2 t2 = _dataRepository.GetById<T2>(primaryId);
            return AutoMapperHelper<T2, T1>.AutoConvert(t2);
        }

        /// <summary>
        /// 按Id集合获取
        /// </summary>
        /// <param name="ids">主键Id集合</param>
        /// <returns>业务对象列表</returns>
        public IEnumerable<T1> GetByIds<T1, T2>(IList<dynamic> ids)
            where T1 : class
            where T2 : class
        {
            IEnumerable<T2> dataList = _dataRepository.GetByIds<T2>(ids);
            IEnumerable<T1> entityList = dataList.Select(Mapper.DynamicMap<T1>);
            return entityList;
        }

        /// <summary>
        /// 全部获取
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T1> GetAll<T1, T2>()
            where T1 : class
            where T2 : class
        {
            IEnumerable<T2> dataList = _dataRepository.GetAll<T2>();
            IEnumerable<T1> entityList = dataList.Select(Mapper.DynamicMap<T1>);
            return entityList;
        }

        /// <summary>
        /// 按条件获取
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public IEnumerable<T1> Get<T1, T2>(string sql, dynamic param = null)
            where T1 : class
            where T2 : class
        {
            IEnumerable<T2> dataList = _dataRepository.Get<T2>(sql, param);
            IEnumerable<T1> entityList = dataList.Select(Mapper.DynamicMap<T1>);
            return entityList;
        }

        /// <summary>
        /// 表达式获取方法
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<TReturn> Get<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null)
        {
            return _dataRepository.Get<TFirst, TSecond, TReturn>(sql, map, param);
        }

        /// <summary>
        /// 分页条件获取，排序
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="allRowsCount"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public IEnumerable<T1> GetPage<T1, T2>(int pageIndex, int pageSize, out long allRowsCount, 
            IPredicate predicate = null, ISort sort = null)
            where T1 : class
            where T2 : class
        {
            IList<ISort> orderBy = new List<ISort>() { sort };
            return GetPage<T1, T2>(pageIndex, pageSize, out allRowsCount, predicate, orderBy);
        }

        /// <summary>
        /// 分页获取，条件排序
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="allRowsCount"></param>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public IEnumerable<T1> GetPage<T1, T2>(int pageIndex, int pageSize, out long allRowsCount, 
            IPredicate predicate = null, IList<ISort> sort = null)
            where T1 : class
            where T2 : class
        {
            IEnumerable<T2> dataList = _dataRepository.GetPage<T2>(pageIndex, pageSize, out allRowsCount, predicate, sort);
            IEnumerable<T1> entityList = dataList.Select(Mapper.DynamicMap<T1>);
            return entityList;
        }

        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int Execute<T1, T2>(string sql, dynamic param = null, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class
        {
            return _dataRepository.Execute(sql, param, transaction);
        }

        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public dynamic Insert<T1, T2>(T1 entity, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class
        {
            T2 item = AutoMapperHelper<T1, T2>.AutoConvert(entity);
            return _dataRepository.Insert<T2>(item, transaction);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="transaction"></param>
        public void InsertBatch<T1, T2>(IEnumerable<T1> entityList, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class
        {
            IEnumerable<T2> dataList = entityList.Select(Mapper.DynamicMap<T2>);
            _dataRepository.InsertBatch<T2>(dataList, transaction);
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Update<T1, T2>(T1 entity, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class
        {
            var item = AutoMapperHelper<T1, T2>.AutoConvert(entity);
            return _dataRepository.Update<T2>(item, transaction);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool UpdateBatch<T1, T2>(IEnumerable<T1> entityList, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class
        {
            IEnumerable<T2> dataList = entityList.Select(Mapper.DynamicMap<T2>);
            return _dataRepository.UpdateBatch<T2>(dataList, transaction);
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="primaryId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Delete<T1, T2>(dynamic primaryId, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class
        {
            return _dataRepository.Delete<T2>(primaryId, transaction);
        }

        /// <summary>
        /// 条件删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Delete<T1, T2>(IPredicate predicate, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class
        {
            return _dataRepository.Delete<T2>(predicate, transaction);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool DeleteBatch<T1, T2>(IEnumerable<dynamic> ids, IDbTransaction transaction = null)
            where T1 : class
            where T2 : class
        {
            return _dataRepository.DeleteBatch<T2>(ids, transaction);
        }
    }
}
