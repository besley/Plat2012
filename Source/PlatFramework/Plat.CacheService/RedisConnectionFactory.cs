using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookSleeve;

namespace Plat.CacheService
{
    /// <summary>
    /// 创建Redis Connection
    /// </summary>
    public sealed class RedisConnectionFactory
    {
        private const string RedisConnectionFailed = "Redis 连接失败!";
        private RedisConnection _connection;
        private static volatile RedisConnectionFactory _instance;

        private static object syncLock = new object();
        private static object syncConnectionLock = new object();

        /// <summary>
        /// 当前唯一实例
        /// </summary>
        public static RedisConnectionFactory Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new RedisConnectionFactory();
                        }
                    }
                }
                return _instance;
            }
        }

        private RedisConnectionFactory()
        {
            _connection = getNewConnection();
        }

        /// <summary>
        /// 创建连接实例
        /// </summary>
        /// <returns></returns>
        private static RedisConnection getNewConnection()
        {
            string serverName = System.Configuration.ConfigurationManager.AppSettings["RedisCacheServerName"];
            string tcpPort = System.Configuration.ConfigurationManager.AppSettings["RedisCacheServerTcpPort"];
            return new RedisConnection(serverName, int.Parse(tcpPort));
        }

        /// <summary>
        /// 获取Redis连接
        /// </summary>
        /// <returns></returns>
        public RedisConnection GetConnection()
        {
            lock (syncConnectionLock)
            {
                if (_connection == null)
                    _connection = getNewConnection();

                if (_connection.State == RedisConnectionBase.ConnectionState.Opening)
                    return _connection;

                if (_connection.State == RedisConnectionBase.ConnectionState.Closing ||
                    _connection.State == RedisConnectionBase.ConnectionState.Closed)
                {
                    try
                    {
                        _connection = getNewConnection();
                    }
                    catch (System.Exception ex)
                    {
                        throw new System.Exception(RedisConnectionFailed, ex);
                    }
                }

                if (_connection.State == RedisConnectionBase.ConnectionState.Shiny)
                {
                    try
                    {
                        var openAsync = _connection.Open();
                        _connection.Wait(openAsync);
                    }
                    catch (System.Net.Sockets.SocketException ex)
                    {
                        throw new System.Exception(RedisConnectionFailed, ex);
                    }
                }
                return _connection;
            }
        }
    }
}