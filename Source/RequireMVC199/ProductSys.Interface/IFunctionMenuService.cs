using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plat.WebUtility;
using Plat.DataRepository;
using Plat.ServiceBuilder;
using ProductSys.BizEntity;

namespace ProductSys.Interface
{
    /// <summary>
    /// 功能菜单服务类
    /// </summary>
    public interface IFunctionMenuService: IService
    {
        /// <summary>
        /// 获取树节点Json格式数据数组
        /// </summary>
        /// <returns></returns>
        JsTreeModel[] GetJsTreeView();
    }
}