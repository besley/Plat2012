using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Plat.DataRepository;

namespace TR.Datasource
{
    public class StorageDatabase : Database, IStorageDatabase
    {
        public StorageDatabase(IDbConnection connection)
            : base(connection)
        {
        }
    }
}