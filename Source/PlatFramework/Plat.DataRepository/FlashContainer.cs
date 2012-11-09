using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plat.CacheService;

namespace Plat.DataRepository
{
    public class FlashContainer
    {
        public static void FlashCachedItems(string key, IList<object> ids)
        {
            RedisManager rm = new RedisManager();
            foreach (var item in ids)
            {
                
            }
        }

    }
}
