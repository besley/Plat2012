using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using ProductSys.Interface;

namespace ProductSysConsole.MEF
{
    public class MEFSample
    {

        public void Run()
        {
            AggregateCatalog catlog = new AggregateCatalog();
            catlog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catlog.Catalogs.Add(new DirectoryCatalog("\\Plugins"));

            CompositionContainer compContainer = new CompositionContainer(catlog);
            compContainer.ComposeParts(this);
            //orderService.InsertWith(null);
        }
    }
}