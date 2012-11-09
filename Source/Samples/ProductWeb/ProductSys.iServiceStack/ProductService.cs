using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using ServiceStack.ServiceHost;
using AutoMapper;
using Plat.DataRepository;
using Plat.ExceptionHelper;
using Plat.LogService;
using ProductSys.DAL;
using ProductSys.iServiceStack.DataContract;

namespace ProductSys.iServiceStack
{
    public partial class ProductService  : IRestService<Product>
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

        public object Get(Product request)
        {
            throw new NotImplementedException();
        }

        public object Post(Product request)
        {
            throw new NotImplementedException();
        }

        public object Put(Product request)
        {
            throw new NotImplementedException();
        }

        public object Delete(Product request)
        {
            throw new NotImplementedException();
        }

        public object Patch(Product request)
        {
            throw new NotImplementedException();
        }
    }
}

/*
 * namespace ServiceStack.Hello
{

	/// Create your Web Service implementation 
	public class HelloService : IService<Hello>
	{
		public object Execute(Hello request)
		{
			return new HelloResponse { Result = "Hello, " + request.Name };
		}
	}


	public class Global : System.Web.HttpApplication
	{
		/// Web Service Singleton AppHost
		public class HelloAppHost : AppHostBase
		{
			//Tell Service Stack the name of your application and where to find your web services
			public HelloAppHost() 
				: base("Hello Web Services", typeof(HelloService).Assembly) { }

			public override void Configure(Funq.Container container) { }
		}

		protected void Application_Start(object sender, EventArgs e)
		{
			//Initialize your application
			var appHost = new HelloAppHost();
			appHost.Init();
		}
	}


}
 */
