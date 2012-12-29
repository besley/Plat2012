using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using Plat.DataRepository;
using Plat.ServiceBuilder;
using ProductSys.Interface;
using ProductSys.BizEntity;
using ProductSys.DAL;

namespace ProductSys.PlugIn
{
    public partial class StorageCheckService : ServiceBase, IStorageCheckService
    {

    }
}