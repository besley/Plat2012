using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace RequireMVC199.RequireJS
{
    public class RequireJSOptions
    {
        private readonly Controller _controller;
        private readonly Dictionary<string, object> _websiteOptions;
        private readonly Dictionary<string, object> _pageOptions;

        public RequireJSOptions(Controller controller)
        {
            _controller = controller;
            _pageOptions = new Dictionary<string, object>();
            _websiteOptions = new Dictionary<string, object>();
            
        }

        public void Add(string key, object value, RequireJSOptionsScope scope = RequireJSOptionsScope.Page)
        {
            switch (scope)
            {
                case RequireJSOptionsScope.Page:
                    if (_pageOptions.Keys.Contains(key))
                    {
                        _pageOptions.Remove(key);
                    }
                    _pageOptions.Add(key, JsonConvert.SerializeObject(value));
                    break;
                case RequireJSOptionsScope.Website:
                    if (_websiteOptions.Keys.Contains(key))
                    {
                        _websiteOptions.Remove(key);
                    }
                    _websiteOptions.Add(key, JsonConvert.SerializeObject(value));
                    break;
            }
        }

        public void Clear(RequireJSOptionsScope scope)
        {
            switch (scope)
            {
                case RequireJSOptionsScope.Page:
                    _pageOptions.Clear();
                    break;
                case RequireJSOptionsScope.Website:
                    _websiteOptions.Clear();
                    break;
            }
        }

        public void ClearAll()
        {
            _pageOptions.Clear();
            _websiteOptions.Clear();

        }

        public void Save(RequireJSOptionsScope scope)
        {
            switch (scope)
            {
                case RequireJSOptionsScope.Page:
                    _controller.ViewBag.PageOptions = new MvcHtmlString(ConvertToJSObject(_pageOptions));
                    break;
                case RequireJSOptionsScope.Website:
                    _controller.ViewBag.WebsiteOptions = new MvcHtmlString(ConvertToJSObject(_websiteOptions));
                    break;
            }
        }

        public void SaveAll()
        {
            _controller.ViewBag.PageOptions = new MvcHtmlString(ConvertToJSObject(_pageOptions));
            _controller.ViewBag.WebsiteOptions = new MvcHtmlString(ConvertToJSObject(_websiteOptions));
        }

        private static string ConvertToJSObject(Dictionary<string, object> options)
        {
            var config = new StringBuilder(1024);
            config.Append("{");
            foreach (var item in options)
            {
                config.AppendFormat(" {0}: {1}{2}", item.Key, item.Value, options.Last().Equals(item) ? "" : ",");
            }
            config.Append("}");
            return config.ToString();
        }

    }

    public enum RequireJSOptionsScope
    {
        Page,
        Website
    }
}

