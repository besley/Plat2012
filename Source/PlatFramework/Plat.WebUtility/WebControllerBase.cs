using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace Plat.WebUtility
{
    public abstract class WebControllerBase : Controller
    {
        /// <summary>
        /// 登录用户对象
        /// </summary>
        public UserEntity LogonUser
        {
            get;
            set;
        }

        public string ApiUrl
        {
            get;
            protected set;
        }

        //protected string Update<T>()
        //{

        //}

        //protected string UpdateBatch<T>()
        //{

        //}
    }
}
