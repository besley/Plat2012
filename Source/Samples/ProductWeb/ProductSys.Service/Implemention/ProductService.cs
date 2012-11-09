using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using Plat.WebUtility;
using Plat.LogService;
using Plat.ExceptionHelper;
using ProductSys.Service.DataContract;
using ProductSys.Service.ServiceContract;
using ProductSys.BLL;
using ProductSys.DAL;
using AutoMapper;

namespace ProductSys.Service.Implemention
{
    public class ProductService : IProductService, IDisposable
    {
        private ProductRepository instance;
        private ProductRepository ProductRepository
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductRepository();
                    System.Diagnostics.Debug.WriteLine("hello my world!");
                }
                return instance;
            }
        }

        public IEnumerable<Product> GetProducts()
        {
            try
            {
                IEnumerable<EPProduct> pList = ProductRepository.GetAll();
                AutoMapper.Mapper.CreateMap<EPProduct, Product>();
                return AutoMapper.Mapper.Map<IEnumerable<EPProduct>, IEnumerable<Product>>(pList);
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("获取产品信息发生异常！", 1002);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                throw new WebFaultException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
            }
        }

        public Product Get(string id)
        {
            try
            {
                EPProduct p = ProductRepository.Get(long.Parse(id));
                AutoMapper.Mapper.CreateMap<EPProduct, Product>();
                //throw new ApplicationException("this is test error message");
                return AutoMapper.Mapper.Map<EPProduct, Product>(p);
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("获取产品信息发生异常！", 1002);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                throw new WebFaultException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
            }
        }

        public void Insert(Product entity)
        {
            try
            {
                AutoMapper.Mapper.CreateMap<Product, EPProduct>();
                EPProduct p = AutoMapper.Mapper.Map<Product, EPProduct>(entity);
                ProductRepository.Insert(p);
                SaveChanges();
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("插入产品信息发生异常！", 1003);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                throw new WebFaultException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
            }
        }

        public void Update(Product entity)
        {
            try
            {
                AutoMapper.Mapper.CreateMap<Product, EPProduct>();
                EPProduct p = AutoMapper.Mapper.Map<Product, EPProduct>(entity);
                ProductRepository.Update(p);
                SaveChanges();
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("更新产品信息发生异常！", 1004);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                throw new WebFaultException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
            }
        }

        public void Delete(string id)
        {
            try
            {
                ProductRepository.Delete(id);
                SaveChanges();
            }
            catch (System.Exception ex)
            {
                //记录异常日志信息
                var errorHandler = ErrorHandlerFactory.Create("删除产品信息发生异常！", 1005);
                LogHelper.Error(errorHandler.GetInfo(), ex);
                throw new WebFaultException<ErrorHandler>(errorHandler, HttpStatusCode.BadRequest);
            }
        }


        private void SaveChanges()
        {
            ProductRepository.SaveChanges();
        }

        public void Dispose()
        {
            if (ProductRepository != null)
            {
                //System.Diagnostics.Debug.WriteLine("I was disposed...");
                ProductRepository.Dispose();
            }
        }
    }
}
