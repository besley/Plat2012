using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequireMVC199.RequireJS
{
    public class RequireJSConfigAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var ctrl = (RequireJSController)filterContext.Controller;

                ctrl.RegisterGlobalOptions();
                ctrl.RequireJSOptions.Save(RequireJSOptionsScope.Website);
                base.OnActionExecuting(filterContext);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var ctrl = (RequireJSController)filterContext.Controller;
                ctrl.RequireJSOptions.Save(RequireJSOptionsScope.Page);
            }
            base.OnActionExecuted(filterContext);
        }
    }
}

