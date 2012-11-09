using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plat.ExceptionHelper
{
    public class ErrorHandlerFactory
    {
        public static ErrorHandler Create(string message, int errorCode)
        {
            return Create(message, errorCode, Guid.NewGuid().ToString());
        }

        public static ErrorHandler Create(string message, int errorCode, string errorGuid)
        {
            ErrorHandler errHandler = new ErrorHandler { ErrorCode = errorCode, ErrorGuid = errorGuid, Cause = message };
            return errHandler;
        }

    }
}
