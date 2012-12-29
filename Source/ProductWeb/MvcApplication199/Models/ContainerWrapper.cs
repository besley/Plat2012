using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleInjector;
using SimpleInjector.Extensions;
using Plat.ServiceBuilder;
using Plat.ServiceBuilder.Injection;

namespace MvcApplication199.Models
{
    public class ContainerWrapper : IContainer
    {
        protected Container Container
        {
            get;
            set;
        }

        public T GetInstance<T>() where T : class
        {
            return Container.GetInstance<T>();
        }

        public ContainerWrapper(Container container)
        {
            Container = container;
        }
    }
}