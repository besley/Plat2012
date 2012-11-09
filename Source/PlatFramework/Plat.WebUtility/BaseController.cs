using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Extensions;
using Plat.DataRepository;

namespace Plat.WebUtility
{
    /// <summary>
    /// Controller的基类，用于实现适合业务场景的基础功能
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseController : ApiController
    {

    }
}

