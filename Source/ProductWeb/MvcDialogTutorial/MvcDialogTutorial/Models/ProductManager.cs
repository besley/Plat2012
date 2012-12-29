using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcDialogTutorial.Models
{
    public class ProductManager
    {
        public IList<Product> ProductList
        {
            get
            {
                if (HttpContext.Current.Items["ProductList"] == null)
                    HttpContext.Current.Items["ProductList"] = new List<Product>();

                return (IList<Product>)HttpContext.Current.Items["ProductList"];
            }
        }

        public ProductManager()
        {
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Book",
                Notes = "Carton book"
            };
            ProductList.Add(product);
            product = new Product
            {
                ProductId = 2,
                ProductName = "Food",
                Notes = "Super Market Food"
            };
            ProductList.Add(product);
        }

        public Product Get(int id)
        {
            return ProductList.Single<Product>(p=>p.ProductId == id);
        }

        public IEnumerable<Product> Get()
        {
            return ProductList;
        }

        public int Save(Product product)
        {
            var ep = ProductList.SingleOrDefault<Product>(p=>p.ProductId == product.ProductId);
            if (ep == null)
                return SaveAsNew(product);
            else
            {
                ProductList.Remove(ep);
                ProductList.Add(product);
                return ep.ProductId;
            }
        }

        public int SaveAsNew(Product product)
        {
            ProductList.Add(product);
            return product.ProductId;
        }
    }
}