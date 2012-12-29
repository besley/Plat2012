using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.Composition;
using Plat.WebUtility;
using Plat.DataRepository;
using Plat.ServiceBuilder;
using ProductSys.BizEntity;

namespace ProductSys.Interface
{
    /// <summary>
    /// 插件服务接口
    /// </summary>
    [InheritedExport(typeof(IStorageCheckService))]
    public interface IStorageCheckService: IService, IPluginService
    {

    }
}