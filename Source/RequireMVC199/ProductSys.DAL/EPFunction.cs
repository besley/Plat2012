using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSys.DAL
{
    public partial class EPFunction
    {
        public int ID { get; set; }
        public string FuncName { get; set; }
        public int ParentFuncId { get; set; }
        public string Notes { get; set; }
    }
}
