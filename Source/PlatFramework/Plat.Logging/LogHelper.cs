using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace Plat.Logging
{
    /// <summary>
    /// 日志记录类
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// Logger 实例
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(typeof(LogHelper));

        /// <summary>
        /// 写入普通信息
        /// </summary>
        /// <param name="message">消息</param>
        public static void Info(string message)
        {
            RecordLog(Logger.Info, message);
        }

        /// <summary>
        /// 写入警告信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Warn(string message, Exception ex)
        {
            RecordLog(Logger.Warn, message, ex);
        }

        /// <summary>
        /// 写入错误信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Error(string message, Exception ex)
        {
            RecordLog(Logger.Error, message, ex);           
        }

        /// <summary>
        /// 写入严重错误信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Fatal(string message, Exception ex)
        {
            RecordLog(Logger.Fatal, message, ex);
        }

        /// <summary>
        /// 写入调试信息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="ex">异常</param>
        public static void Debug(string message, Exception ex)
        {
            RecordLog(Logger.Debug, message, ex);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="method">日志记录类型的方法</param>
        /// <param name="message">消息内容</param>
        private static void RecordLog(Action<string> method, string message)
        {
            ThreadPool.QueueUserWorkItem(x => method(message));
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="method">日志记录类型的方法</param>
        /// <param name="message">消息内容</param>
        /// <param name="ex">异常</param>
        private static void RecordLog(Action<string, Exception> method, string message, Exception ex)
        {
            ThreadPool.QueueUserWorkItem(x => method(message, ex));
            //think about this soluation later
            //Task.Factory.StartNew(() => Logger.Error(message, ex));
        }
    }
}
