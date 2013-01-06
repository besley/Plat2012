using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSys.DAL
{
    /// <summary>
    /// 出入库记录实体
    /// </summary>
    public partial class EPStorageCheck
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public byte CheckTypeId { get; set; }
        public int CheckAmount { get; set; }
        public string CheckPerson { get; set; }
        public System.DateTime CheckDatetime { get; set; }
    }
}
