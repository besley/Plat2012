using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Plat.ServiceBuilder.Injection
{
    public class CompositionContainerWrapper : IContainer
    {
        protected CompositionContainer CompContainer
        {
            get;
            private set;
        }

        private CompositionContainerWrapper(CompositionContainer container)
        {
            CompContainer = container;
        }

        public T GetInstance<T>() where T : class
        {
            return CompContainer.GetExportedValue<T>();
        }

        public static CompositionContainerWrapper Wrapper(CompositionContainer container)
        {
            return new CompositionContainerWrapper(container);
        }
    }
}
