using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data.SqlClient;
using AutoMapper;
using Plat.DataRepository;
using Plat.ExceptionHelper;
using Plat.Logging;
using Plat.WebUtility;
using Plat.ServiceBuilder;
using ProductSys.DAL;
using ProductSys.BizEntity;
using ProductSys.BizEntity.Queryable;
using ProductSys.Interface;
using ProductSys.WebAPI.Core;

namespace ProductSys.WebAPI.Controllers
{
    public class StorageController : ApiControllerBase
    {
        protected IStorageService StorageServiceInstance
        {
            get;
            private set;
        }

        public StorageController()
        {
            StorageServiceInstance = ServiceManager<IStorageService>.CreateService("IStorageService");
            StorageServiceInstance.Initialize(ApplicationMediator.MainContainerWrapperCached,
                ApplicationMediator.StroageDbSessionCached);
        }

        // GET api/product/5
        [HttpGet]
        public Storage Get(string id)
        {
            try
            {
                var p = StorageServiceInstance.GetStorage(long.Parse(id));
                //throw new ApplicationException("this is test error message");
                if (p == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("id3 not found"), ReasonPhrase = "product id not exist." });
                }
                return p;
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息

                var errorHandler = ErrorHandlerFactory.Create("获取库存信息发生异常！", 1002);
                LogHelper.Error(errorHandler.GetInfo(), ex);

                if (ex.InnerException != null)
                    throw ex.InnerException;
                else
                    throw ex;
            }
        }

        [HttpGet]
        public IEnumerable<Storage> GetAll()
        {
            try
            {
                var pList = StorageServiceInstance.GetAll<Storage, EPStorage>();
                //throw new ApplicationException("this is test error message");
                if (pList == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("id3 not found"), ReasonPhrase = "product id not exist." });
                }
                return pList;
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息

                var errorHandler = ErrorHandlerFactory.Create("获取库存信息发生异常！", 1002);
                LogHelper.Error(errorHandler.GetInfo(), ex);

                if (ex.InnerException != null)
                    throw ex.InnerException;
                else
                    throw ex;
            }
        }

        [HttpPost]
        public string BanlanceStorage(string id)
        {
            var p = StorageServiceInstance.BanlanceStorage(id);
            return "OK";

        }

        [HttpGet]
        public string Hello()
        {
            try
            {
                var p = StorageServiceInstance.HelloMessage("test sample");
                //throw new ApplicationException("this is test error message");
                if (p == null)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("id3 not found"), ReasonPhrase = "product id not exist." });
                }
                return p;
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息

                var errorHandler = ErrorHandlerFactory.Create("获取库存信息发生异常！", 1002);
                LogHelper.Error(errorHandler.GetInfo(), ex);

                if (ex.InnerException != null)
                    throw ex.InnerException;
                else
                    throw ex;
            }
        }
    }
}
