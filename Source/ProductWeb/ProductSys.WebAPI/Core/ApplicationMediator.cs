using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SimpleInjector;
using Plat.ServiceBuilder.Injection;
using ProductSys.DAL;
using Plat.DataRepository;
using TR.Datasource;

namespace ProductSys.WebAPI.Core
{
    /// <summary>
    /// WebApplication 调节类
    /// </summary>
    public class ApplicationMediator
    {
        private static readonly string _webApiProductDbSession = "WebApiProductDbSession";
        private static readonly string _webApiStorageDbSession = "WebApiStorageDbSession";
        private static readonly string _webApiMainContainer = "WebApiMainContainer";
        private static readonly string _webApiMainContainerWrapper = "WebApiMainContainerWrapper";
        private static readonly string _webApiPluginContainerWrapper = "WebApiPluginContainerWrapper";

        /// <summary>
        /// 主容器缓存对象
        /// </summary>
        internal static Container MainContainerCached
        {
            get
            {
                var container = HttpContext.Current.Items[_webApiMainContainer] as Container;
                if (container == null)
                {
                    throw new ApplicationException("MainContainer缓存对象已经无效，请检查具体原因！");
                }
                return container;
            }
        }

        /// <summary>
        /// 主容器封装对象缓存
        /// </summary>
        internal static IContainer MainContainerWrapperCached
        {
            get
            {
                var container = HttpContext.Current.Items[_webApiMainContainerWrapper] as IContainer;
                if (container == null)
                {
                    throw new ApplicationException("MainContainerWrapper缓存对象已经无效，请检查具体原因！");
                }
                return container;
            }
        }

        /// <summary>
        /// 插件容器封装缓存
        /// </summary>
        internal static IContainer PluginContainerWrapperCached
        {
            get
            {
                var container = HttpContext.Current.Items[_webApiPluginContainerWrapper] as IContainer;
                if (container == null)
                {
                    throw new ApplicationException("PluginContainerWrapper缓存对象已经无效，请检查具体原因！");
                }
                return container;
            }
        }

        /// <summary>
        /// 产品数据库Session缓存
        /// </summary>
        internal static ISession ProductDbSessionCached
        {
            get
            {
                var session = HttpContext.Current.Items[_webApiProductDbSession] as ISession;
                if (session == null)
                {
                    throw new ApplicationException("ProductDbSession缓存对象已经无效，请检查具体原因！");
                }
                return session;
            }
        }

        /// <summary>
        /// 库存数据库Session缓存
        /// </summary>
        internal static ISession StroageDbSessionCached
        {
            get
            {
                var session = HttpContext.Current.Items[_webApiStorageDbSession] as ISession;
                if (session == null)
                {
                    throw new ApplicationException("StorageDbSession缓存对象已经无效，请检查具体原因！");
                }
                return session;
            }
        }

        /// <summary>
        /// 注册产品数据库Session对象
        /// </summary>
        private static void RegisterProductDbSession()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ProductDbContext"].ToString();
            if (String.IsNullOrEmpty(connectionString))
                throw new ApplicationException("数据库连接字符串不能为空！请检查！");

            IDbConnection connection = new SqlConnection(connectionString);

            MainContainerCached.Register<IProductDatabase>(() =>
            {
                var database = new ProductDatabase(connection);
                return database;
            });

            MainContainerCached.Register<IProductDbSession, ProductDbSession>();
        }

        /// <summary>
        /// 注册库存数据库Session对象
        /// </summary>
        private static void RegisterStroageDbSession()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["StorageDbContext"].ToString();
            if (String.IsNullOrEmpty(connectionString))
                throw new ApplicationException("数据库连接字符串不能为空！请检查！");

            IDbConnection connection = new SqlConnection(connectionString);

            MainContainerCached.Register<IStorageDatabase>(() =>
            {
                var database = new StorageDatabase(connection);
                return database;
            });

            MainContainerCached.Register<IStorageDbSession, StorageDbSession>();
        }

        /// <summary>
        /// 保存session对象到缓存 
        /// </summary>
        internal static void KeepSessionCached()
        {
            var productSession = MainContainerCached.GetInstance<IProductDbSession>();
            HttpContext.Current.Items[_webApiProductDbSession] = productSession;

            var storageSession = MainContainerCached.GetInstance<IStorageDbSession>();
            HttpContext.Current.Items[_webApiStorageDbSession] = storageSession;
        }

        /// <summary>
        /// 页面请求开始执行操作
        /// </summary>
        internal static void BeginRequest()
        {
            RegisterServiceDependency();
            RegisterProductDbSession();
            RegisterStroageDbSession();
            KeepSessionCached();
        }

        /// <summary>
        /// 释放Session资源
        /// </summary>
        private static void DisposeProductDbSession()
        {
            if (HttpContext.Current.Items[_webApiProductDbSession] != null)
            {
                ISession session = (ISession)HttpContext.Current.Items[_webApiProductDbSession];
                if (session != null)
                {
                    try
                    {
                        if (session.Transaction != null)
                        {
                            session.Transaction.Commit();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        session.Transaction.Rollback();
                    }
                    finally
                    {
                        session.Connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 释放session资源
        /// </summary>
        private static void DisposeStorageDbSession()
        {
            if (HttpContext.Current.Items[_webApiProductDbSession] != null)
            {
                ISession session = (ISession)HttpContext.Current.Items[_webApiProductDbSession];
                if (session != null)
                {
                    try
                    {
                        if (session.Transaction != null)
                        {
                            session.Transaction.Commit();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        session.Transaction.Rollback();
                    }
                    finally
                    {
                        session.Connection.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 页面请求结束执行操作
        /// </summary>
        internal static void EndRequest()
        {
            DisposeProductDbSession();
            DisposeStorageDbSession();
        }

        /// <summary>
        /// 注册DbContext对象，服务对象，插件对象等
        /// </summary>
        internal static void RegisterServiceDependency()
        {
            //注册Service and Repository
            var container = new Container();
            var containerWrapper = ContainerWrapper.Wrapper(container);

            ServiceContainerManager.RegisterService(container, containerWrapper);

            HttpContext.Current.Items[_webApiMainContainer] = container;
            HttpContext.Current.Items[_webApiMainContainerWrapper] = containerWrapper;

            //注册plugin container
            var compContainer = PluginManager.Register();
            var pluginContainerWrapper = CompositionContainerWrapper.Wrapper(compContainer);
            HttpContext.Current.Items[_webApiPluginContainerWrapper] = pluginContainerWrapper;
        }
    }
}