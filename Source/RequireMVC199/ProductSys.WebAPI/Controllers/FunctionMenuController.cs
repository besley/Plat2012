using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Plat.ExceptionHelper;
using Plat.DataRepository;
using Plat.WebUtility;
using Plat.ServiceBuilder;
using Plat.Logging;
using ProductSys.DAL;
using ProductSys.BizEntity;
using ProductSys.BizEntity.Queryable;
using ProductSys.Interface;
using ProductSys.WebAPI.Core;

namespace ProductSys.WebAPI.Controllers
{
    public class FunctionMenuController : ApiControllerBase
    {
        protected IFunctionMenuService FunctionMenuServiceInstance
        {
            get;
            private set;
        }

        public FunctionMenuController()
        {
            FunctionMenuServiceInstance = ServiceManager<IFunctionMenuService>.CreateService("IFunctionMenuService");
        }

        [HttpGet]
        public JsTreeModel[] GetJsTreeView()
        {
            return FunctionMenuServiceInstance.GetJsTreeView();
        }
    }
}
