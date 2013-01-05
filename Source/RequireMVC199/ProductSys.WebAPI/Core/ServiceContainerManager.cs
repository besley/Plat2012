using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using SimpleInjector;
using Plat.ServiceBuilder.Injection;
using Plat.DataRepository;
using ProductSys.DAL;
using ProductSys.Interface;
using ProductSys.ServiceImp;

namespace ProductSys.WebAPI.Core
{
    /// <summary>
    /// 服务容器管理类
    /// </summary>
    public class ServiceContainerManager
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="container">容器</param>
        /// <param name="containerWrapper">容器封装类</param>
        public static void RegisterService(Container container, IContainer containerWrapper)
        {
            //注册产品服务类
            container.Register<IProductService>(() =>
            {
                var productService = new ProductService();
                productService.Initialize(containerWrapper, ApplicationMediator.ProductDbSessionCached);
                return productService;
            });

            //注册订单服务类
            container.Register<IOrderService>(() =>
            {
                var orderService = new OrderService();
                orderService.Initialize(containerWrapper, ApplicationMediator.ProductDbSessionCached);
                return orderService;
            });

            //注册功能菜单服务类
            container.Register<IFunctionMenuService>(() =>
            {
                var functionService = new FunctionMenuService();
                functionService.Initialize(containerWrapper, ApplicationMediator.ProductDbSessionCached);
                return functionService;

            });
        }
    }
}
