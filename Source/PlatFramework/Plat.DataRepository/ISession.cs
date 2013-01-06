using System;
using System.ComponentModel;
using System.Data;
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
    /// 数据库接口
    /// </summary>
    public interface IDatabase
    {
        IDbConnection Connection { get; }
    }

    /// <summary>
    /// 数据库类对象
    /// </summary>
    public abstract class Database : IDatabase
    {
        private IDbConnection _connection;
        public IDbConnection Connection
        {
            get { return _connection; }
        }

        public Database(IDbConnection connection)
        {
            _connection = connection;
        }
    }

    /// <summary>
    /// 数据连接事务的Session接口
    /// </summary>
    public interface ISession : IDisposable
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }

        IDbTransaction Begin(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void Commit();
        void Rollback();
    }
}
