using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.Composition;
using Plat.DataRepository;
using Plat.ServiceBuilder;
using ProductSys.BizEntity;

namespace ProductSys.Interface
{
    /// <summary>
    /// 插件服务接口
    /// </summary>
    [InheritedExport(typeof(IStorageService))]
    public interface IStorageService: IService, IPluginService
    {
        Storage GetStorage(object id);
        string HelloMessage(string message);
        bool BanlanceStorage(dynamic id);
    }
}