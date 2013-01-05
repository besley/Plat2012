using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plat.DataRepository;

namespace TR.Datasource
{
    public class StorageDbSession : SessionBase, IStorageDbSession
    {
        public StorageDbSession(IStorageDatabase database)
            :base(database)
        {
        }
    }
}