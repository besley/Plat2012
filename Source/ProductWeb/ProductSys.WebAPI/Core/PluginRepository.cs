using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.Http;
using System.IO;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using ProductSys.Interface;

namespace ProductSys.WebAPI.Core
{
    /// <summary>
    /// 插件服务存储类
    /// </summary>
    public class PluginRepository
    {
        /// <summary>
        /// 读取XML配置信息，判断是否是插件服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static bool IsPluginService(string serviceName)
        {
            bool isPlugin = false;
            string configXmlPath = HttpContext.Current.Server.MapPath("~\\Plugins\\PluginsConfig.xml");
            using (var streamReader = new System.IO.StreamReader(configXmlPath))
            {
                XDocument xDoc = XDocument.Load(streamReader);
                XElement plugin = (from xml2 in xDoc.Elements("Plugins").Elements("Plugin")
                                   where xml2.Attribute("name").Value == serviceName
                                   select xml2).FirstOrDefault();
                isPlugin = (plugin != null);
            }
            return isPlugin;
        }

        //[Import("IStorageService")]
        //public IStorageService StorageService { get; set; }

        //[Import("IStorageCheckService")]
        //public IStorageCheckService StorageCheckService { get; set; }
    }
}