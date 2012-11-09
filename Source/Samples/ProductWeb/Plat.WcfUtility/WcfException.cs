using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plat.WcfUtility
{
    /// <summary>
    /// Wcf异常类
    /// </summary>
    public class WcfException : ApplicationException
    {
        public WcfException(string message)
            : base(message)
        {
        }

        public WcfException(string message, Exception ex)
            : base(message, ex)
        {

        }
    }
}
