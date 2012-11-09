using System;
using System.Collections.Generic;
using System.Linq;
using LINQExtensions;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace Plat.DataRepository
{
    /// <summary>
    /// 数据持久化操作类
    /// </summary>
    /// <typeparam name="TDataEntity">数据实体类型</typeparam>
    public abstract class RepositoryBase<TDataEntity> : IDataRepository<TDataEntity>
        where TDataEntity : class
    {
        #region 抽象操作
        /// <summary>
        /// 获取DbContext的抽象方法
        /// </summary>
        /// <returns>DbContext</returns>
        protected abstract DbContext CreateDbContext();
        #endregion

        #region 属性及构造
        private DbContext dbContext = null;
        /// <summary>
        /// DbContext 实例
        /// </summary>
        public DbContext DbContext
        {
            get { return dbContext; }
        }

        private DbSet<TDataEntity> dbSet;
        /// <summary>
        /// DbSet 实例
        /// </summary>
        public DbSet<TDataEntity> DbSet
        {
            get { return dbSet; }
        }

        private IList<object> ids;
        /// <summary>
        /// 要缓存的ID列表
        /// </summary>
        public IList<object> IDs
        {
            get { return ids; }
        }

        private Action<IList<object>> flashCache;
        /// <summary>
        /// 更新缓存事件
        /// </summary>
        public Action<IList<object>> FlashCache
        {
            get { return flashCache; }
        }
        
        /// <summary>
        /// 构造RepositoryBase
        /// </summary>
        public RepositoryBase()
        {
            if (dbContext == null)
                dbContext = CreateDbContext();

            this.dbSet = dbContext.Set<TDataEntity>();

            if (ids == null)
                ids = new List<object>();

            //注册缓存事件
            this.flashCache = items => FlashContainer.FlashCachedItems(GetDataEntityType().FullName,
                items);
        }

        /// <summary>
        /// 获取数据实体类型名称
        /// </summary>
        /// <returns>类型</returns>
        public Type GetDataEntityType()
        {
            return typeof(TDataEntity);
        }
        #endregion

        #region 数据记录Get操作
        /// <summary>
        /// 根据主键ID获取记录
        /// </summary>
        /// <param name="primaryID">主键ID</param>
        /// <returns>实体对象</returns>
        public virtual TDataEntity Get(object primaryID)
        {
            return dbSet.Find(primaryID);
        }

        /// <summary>
        /// 获取全部数据集（适用于少量数据的数据表）
        /// </summary>
        /// <returns>列表</returns>
        public virtual IEnumerable<TDataEntity> GetAll()
        {
            return dbSet.ToList<TDataEntity>();
        }

        /// <summary>
        /// 根据多个主键ID获取数据集合, 类似 in 操作
        /// </summary>
        /// <param name="ids">主键ID列表</param>
        /// <returns>列表集合</returns>
        public virtual IEnumerable<TDataEntity> GetByIds(IList<object> ids)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据条件筛选数据
        /// </summary>
        /// <param name="where">where条件语句</param>
        /// <param name="values">参数</param>
        /// <returns>列表集合</returns>
        public virtual IEnumerable<TDataEntity> Get(string where, params object[] values)
        {
            return dbSet.SqlQuery(where, values);
        }

        /// <summary>
        /// 根据条件获取记录
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="allRowsCount">总记录数</param>
        /// <param name="where">查询条件</param>
        /// <param name="values">参数</param>
        /// <returns>记录集合</returns>
        public virtual IEnumerable<TDataEntity> Get(int pageIndex, 
            int pageSize, out long allRowsCount, string where, 
            params object[] values)
        {
            return Get(pageIndex, pageSize, out allRowsCount, string.Empty, where, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="allRowsCount">总记录数</param>
        /// <param name="orderByFieldName">排序字段名称</param>
        /// <param name="where">查询条件</param>
        /// <param name="values">参数</param>
        /// <returns>记录集合</returns>
        public virtual IEnumerable<TDataEntity> Get(int pageIndex, 
            int pageSize, out long allRowsCount, string orderByFieldName, 
            string where, params object[] values)
        {
            return Get(pageIndex, pageSize, out allRowsCount, orderByFieldName, SortType.Asc, where, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="allRowsCount">总记录数</param>
        /// <param name="orderByFieldName">排序字段名称</param>
        /// <param name="sortType">升序或降序</param>
        /// <param name="where">查询条件</param>
        /// <param name="values">参数</param>
        /// <returns>记录集合</returns>
        public virtual IEnumerable<TDataEntity> Get(int pageIndex, 
            int pageSize, out long allRowsCount, string orderByFieldName, SortType sortType, 
            string where, params object[] values)
        {
            string orderByExpression = string.Empty;
            if (sortType == SortType.Desc)
            {
                orderByExpression = string.Format("{0} DESC", orderByFieldName);
            }
            return GetOrderByExpression(pageIndex, pageSize, out allRowsCount, orderByExpression, where, values);
        }

        /// <summary>
        /// 获取有排序表达式的记录
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="allRowsCount">总记录数</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="where">查询条件</param>
        /// <param name="values">参数</param>
        /// <returns>记录集合</returns>
        public IEnumerable<TDataEntity> GetOrderByExpression(int pageIndex, 
            int pageSize, out long allRowsCount, string orderByExpression, 
            string where, params object[] values)
        {
            IQueryable<TDataEntity> query;

            if (pageIndex < 0)
                pageIndex = 0;

            if (pageSize < 0)
                pageSize = 10;

            if (where != string.Empty)
                query = dbSet.SqlQuery(where, values).AsQueryable<TDataEntity>();
            else
                query = dbSet.AsQueryable<TDataEntity>();

            allRowsCount = query.Count();

            int skipCount = ((pageIndex - 1) < 0) ? 0 : (pageIndex - 1) * pageSize;
            if (orderByExpression != string.Empty)
                query = query.Skip<TDataEntity>(skipCount).Take<TDataEntity>(pageSize).OrderUsingSortExpression<TDataEntity>(orderByExpression);
            else
                query = query.Skip<TDataEntity>(skipCount).Take<TDataEntity>(pageSize);

            return query;
        }
        #endregion

        #region 数据增删改操作
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="entityToInsert">要插入的实体</param>
        /// <returns></returns>
        public virtual TDataEntity Insert(TDataEntity entityToInsert)
        {
            return dbSet.Add(entityToInsert);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="entityToUpdate">要更新的实体</param>
        public virtual void Update(TDataEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            dbContext.Entry<TDataEntity>(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// 根据主键ID删除记录
        /// </summary>
        /// <param name="primaryID">主键ID</param>
        public virtual void Delete(object primaryID)
        {
            TDataEntity entityToDelete = dbSet.Find(primaryID);
            Delete(entityToDelete);
        }

        /// <summary>
        /// 删除数据记录
        /// </summary>
        /// <param name="entityToDelete">要删除的实体</param>
        public virtual void Delete(TDataEntity entityToDelete)
        {
            if (dbContext.Entry<TDataEntity>(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
             
        #endregion

        public void Dispose()
        {
            if (this.dbContext != null)
                this.dbContext.Dispose();
        }
    }
}
