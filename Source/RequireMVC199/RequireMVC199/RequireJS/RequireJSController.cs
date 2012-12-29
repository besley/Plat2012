using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequireMVC199.RequireJS
{
    [RequireJSConfig]
    public class RequireJSController : Controller
    {
        private RequireJSOptions _requireJSOptions;
        public RequireJSOptions RequireJSOptions
        {
            get { return _requireJSOptions; }
        }

        public void RegisterGlobalOptions() { }

        public RequireJSController()
        {
            _requireJSOptions = new RequireJSOptions(this);                 
        }

        public string ApiUrl
        {
            get;
            set;
        }
    }
}
