using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSys.DAL
{
    /// <summary>
    /// 库存业务对象
    /// </summary>
    public partial class EPStorage
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public int StorageAmount { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
    }
}
