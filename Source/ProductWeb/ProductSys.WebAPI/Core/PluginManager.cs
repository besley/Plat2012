using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace ProductSys.WebAPI.Core
{
    /// <summary>
    /// 插件管理器
    /// </summary>
    public class PluginManager
    {
        /// <summary>
        /// 注册Plugin目录下的插件服务
        /// </summary>
        /// <returns></returns>
        public static CompositionContainer Register()
        {
            CompositionContainer container = null;

            var catalog = new DirectoryCatalog(@"Plugins");
            container = new CompositionContainer(catalog);
            
            return container;
        }
    }
}