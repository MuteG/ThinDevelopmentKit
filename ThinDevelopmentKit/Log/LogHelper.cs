using System.Collections.Generic;

namespace ThinDevelopmentKit.Log
{
    /// <summary>
    /// 日志助手
    /// </summary>
    public static class LogHelper
    {
        private static List<ILogHelper> logHelperList;

        static LogHelper()
        {
            logHelperList = new List<ILogHelper>();
        }

        public static void AddTarget(LogTarget target, string source)
        {
            AddTarget(target, source, false);
        }

        public static void AddTarget(LogTarget target, string source, bool throwError)
        {
            ILogHelper logHelper = null;
            switch (target)
            {
                case LogTarget.Text:
                    logHelper = new TextLogHelper() { Source = source, ThrowError = throwError };
                    break;
                case LogTarget.EventLog:
                    logHelper = new EventLogHelper() { Source = source, ThrowError = throwError };
                    break;
            }
            if (!logHelperList.Contains(logHelper))
            {
                logHelperList.Add(logHelper);
            }
        }

        public static void RemoveTarget(LogTarget target, string source)
        {
            switch (target)
            {
                case LogTarget.Text:
                    logHelperList.Remove(new TextLogHelper() { Source = source });
                    break;
                case LogTarget.EventLog:
                    logHelperList.Remove(new EventLogHelper() { Source = source });
                    break;
            }
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="level">日志等级</param>
        /// <param name="message">日志信息</param>
        public static void WriteLog(LogLevel level, string message)
        {
            foreach (ILogHelper logHelper in logHelperList)
            {
                logHelper.WriteLog(level, message);
            }
        }
    }
}
