using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Plat.DataRepository;
using Plat.WebUtility;
using Plat.ServiceBuilder.Injection;
using Plat.ServiceBuilder;
using ProductSys.DAL;
using ProductSys.BizEntity;
using ProductSys.Interface;

namespace ProductSys.WebAPI.Core
{
    /// <summary>
    /// 业务模型创建工厂类
    /// </summary>
    public partial class ServiceManager<T>
        where T:class
    {
        /// <summary>
        /// 服务创建方法
        /// </summary>
        /// <param name="serviceName">服务的标识名称</param>
        /// <returns></returns>
        public static T CreateService(string serviceName)
        {
            T service = null;
            if (PluginRepository.IsPluginService(serviceName))
            {
                //注册插件服务
                var pluginContainer = ApplicationMediator.PluginContainerWrapperCached;
                service = pluginContainer.GetInstance<T>();
            }
            else
            {
                //注册系统内置服务
                var container = ApplicationMediator.MainContainerWrapperCached;
                service = container.GetInstance<T>();
            }
            return service;
        }
    }
}