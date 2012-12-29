using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace RequireMVC199.RequireJS
{
    /// <summary>
    /// RequireJS 帮助类
    /// </summary>
    public static class RequireJsHtmlHelpers
    {
        /// <summary>
        /// 获取每个页面的入口JS文件
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static MvcHtmlString RequireJsPagePoint(this HtmlHelper html)
        {
            //var area = html.ViewContext.RouteData.DataTokens["area"] != null
            //           ? html.ViewContext.RouteData.DataTokens["area"].ToString()
            //           : "Product";
            var controller = html.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue as string;
            var action = html.ViewContext.Controller.ValueProvider.GetValue("action").RawValue as string;

            //var entryPoint = "Controllers/" + area + "/" + controller + "/" + controller + "-" + action;
            var entryPoint = "Controllers/" + controller + "/" + controller + "-" + action;
            var filePath = html.ViewContext.HttpContext.Server.MapPath("~/ViewJS/" + entryPoint + ".js");

            return File.Exists(filePath) ? new MvcHtmlString(entryPoint) : null;
        }

        /// <summary>
        /// 输出home, page, form等页面的公共js文件
        /// </summary>
        /// <param name="html"></param>
        /// <param name="entryPageType"></param>
        /// <returns></returns>
        public static MvcHtmlString RequireCommonJsOfWeb(this HtmlHelper html, string entryPageType)
        {
            var entryPoint = string.Empty;
            switch (entryPageType)
            {
                case "Home":
                    entryPoint = "entryOfHome";
                    break;
                case "List":
                    entryPoint = "entryOfList";
                    break;
                case "Form":
                    entryPoint = "entryOfForm";
                    break;
            }

            if (string.IsNullOrEmpty(entryPoint))
                throw new System.ApplicationException("页面类型的入口点不能为空，请重新指定参数！");

            var filePath = html.ViewContext.HttpContext.Server.MapPath("~/ViewJS/" + entryPoint + ".js");

            return File.Exists(filePath) ? new MvcHtmlString("~/ViewJS/" + entryPoint) : null;
        }

        /// <summary>
        /// 输出JS路径
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public static MvcHtmlString GetRequireJsPathsOfPage(this HtmlHelper html, string pageType)
        {
            var configFileName = string.Empty;
            switch (pageType)
            {
                case "Home":
                    configFileName = "~/RequireJSOfHome.config";
                    break;
                case "List":
                    configFileName = "~/RequireJSOfList.config";
                    break;
                case "Form":
                    configFileName = "~/RequireJSOfForm.config";
                    break;
            }
            var result = new StringBuilder();
            var paths = XDocument.Load(html.ViewContext.HttpContext.Server.MapPath(configFileName)).Descendants("paths").Descendants("path");

            result.Append("{");
            foreach (var item in paths)
            {
                result.AppendFormat("\"{0}\":\"{1}\"{2}", item.Attribute("key").Value.Trim(), item.Attribute("value").Value.Trim(), paths.Last() == item ? "" : ",");
            }
            result.Append("}");

            return new MvcHtmlString(result.ToString());
        }

        /// <summary>
        /// 输出JS依赖路径
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public static MvcHtmlString GetRequireJsShimOfPage(this HtmlHelper html, string pageType)
        {
            var configFileName = string.Empty;
            switch (pageType)
            {
                case "Home":
                    configFileName = "~/RequireJSOfHome.config";
                    break;
                case "List":
                    configFileName = "~/RequireJSOfList.config";
                    break;
                case "Form":
                    configFileName = "~/RequireJSOfForm.config";
                    break;
            }

            var result = new StringBuilder();
            var shims = XDocument.Load(html.ViewContext.HttpContext.Server.MapPath(configFileName)).Descendants("shim").Descendants("dependencies");

            result.Append("{");
            foreach (var item in shims)
            {
                result.AppendFormat(" \"{0}\": {1} deps: [", item.Attribute("for").Value.Trim(), "{");
                var deps = item.Descendants("add");
                foreach (var dep in deps)
                {
                    result.AppendFormat("\"{0}\"{1}", dep.Attribute("dependency").Value.Trim(), deps.Last() == dep ? "" : ",");
                }

                var exports = item.Attribute("exports") != null && !string.IsNullOrEmpty(item.Attribute("exports").Value)
                                  ? ", exports: '" + item.Attribute("exports").Value.Trim() + "'"
                                  : string.Empty;

                result.AppendFormat("]{0} {1}{2} ", exports, "}", shims.Last() == item ? "" : ",");
            }
            result.Append("}");

            return new MvcHtmlString(result.ToString());
        }

        /// <summary>
        /// 获取区域代码
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string CurrentCulture(this HtmlHelper html)
        {
            // split the ro-Ro string by '-' so it returns eg. ro / en
            return System.Threading.Thread.CurrentThread.CurrentCulture.Name.Split('-')[0];
        }
    }
}