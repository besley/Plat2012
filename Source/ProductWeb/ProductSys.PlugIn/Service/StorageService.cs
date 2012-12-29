using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;
using Plat.DataRepository;
using Plat.ServiceBuilder;
using ProductSys.Interface;
using ProductSys.BizEntity;
using ProductSys.DAL;
using TR.Datasource;

namespace ProductSys.PlugIn
{
    public partial class StorageService : ServiceBase, IStorageService
    {
        public string HelloMessage(string message)
        {
            return "Hello message from the world!";
        }

        public Storage GetStorage(object id)
        {
            Storage storage = GetById<Storage, EPStorage>(id);

            IProductService productService = base.Container.GetInstance<IProductService>();
            productService.Initialize(base.Container, base.Container.GetInstance<IProductDbSession>());

            Product product = productService.GetById<Product, EPProduct>(storage.ProductId);

            storage.Product = product;
            return storage;
        }

        public bool BanlanceStorage(dynamic id)
        {
            var storage = base.DataRepository.GetById<EPStorage>(id) as EPStorage;
            storage.StorageAmount += 1;
            
            IProductService productService = base.Container.GetInstance<IProductService>();
            productService.Initialize(base.Container, base.Container.GetInstance<IProductDbSession>());

            var product = productService.DataRepository.GetById<EPProduct>(storage.ProductId) as EPProduct;
            product.Notes += " 1 checkout";

            try
            {
                //base.DataRepository.Update<EPStorage>(storage);
                //productService.DataRepository.Update<EPProduct>(product);

                var transaction = base.Session.Begin();
                base.DataRepository.Update<EPStorage>(storage, transaction);
                productService.DataRepository.Update<EPProduct>(product, transaction);
                base.Session.Commit();
            }
            catch (System.Exception ex)
            {
                base.Session.Rollback();
                throw;
            }
            return true;
        }
    }
}