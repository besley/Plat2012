using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductSysConsole.Dapper
{
    public interface IMediator
    {
        T Get<T>() where T:class;
    }
}
