using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Plat.ExceptionHelper
{
    [DataContract]
    public class ErrorHandler
    {
        [DataMember]
        public int ErrorCode { get; set; }

        [DataMember]
        public string ErrorGuid { get; set; }

        [DataMember]
        public string Cause { get; set; }

        public string GetInfo()
        {
            return string.Format("错误描述:{0}, 错误代码:{1}, 错误标识:{2}", Cause, ErrorCode, ErrorGuid); 
        }
    }
}
