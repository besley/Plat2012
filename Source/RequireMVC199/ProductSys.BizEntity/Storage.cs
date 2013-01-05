using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSys.BizEntity
{
    /// <summary>
    /// 库存业务对象
    /// </summary>
    public partial class Storage
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public int StorageAmount { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }

        /// <summary>
        /// 库存记录的产品数据
        /// </summary>
        public Product Product { get; set; }
    }
}
