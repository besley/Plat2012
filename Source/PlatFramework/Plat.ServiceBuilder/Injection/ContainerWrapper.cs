using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleInjector;

namespace Plat.ServiceBuilder.Injection
{
    public class ContainerWrapper : IContainer
    {
        protected Container SimpleContainer
        {
            get;
            private set;
        }

        private ContainerWrapper(Container container)
        {
            SimpleContainer = container;
        }

        public T GetInstance<T>() where T : class
        {
            return SimpleContainer.GetInstance<T>();
        }

        public static ContainerWrapper Wrapper(Container container)
        {
            return new ContainerWrapper(container);
        }
    }
}