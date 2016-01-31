using System;
using System.Diagnostics;

namespace ThinDevelopmentKit.Log
{
    internal class EventLogHelper : AbstractLogHelper
    {
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="level">日志等级</param>
        /// <param name="message">日志信息</param>
        public override void WriteLog(LogLevel level, string message)
        {
            try
            {
                if (string.IsNullOrEmpty(base.Source))
                {
                    throw new System.ArgumentNullException("source");
                }
                if (!EventLog.SourceExists(base.Source))
                {
                    EventLog.CreateEventSource(base.Source, null); //null表示输出到Application类别
                }
                string logText = GetLevelText(level) + message;
                EventLogEntryType logType = EventLogEntryType.Information;
                switch (level)
                {
                    case LogLevel.Normal:
                    case LogLevel.Debug:
                        logType = EventLogEntryType.Information;
                        break;
                    case LogLevel.Warning:
                        logType = EventLogEntryType.Warning;
                        break;
                    case LogLevel.Error:
                        logType = EventLogEntryType.Error;
                        break;
                }
                EventLog.WriteEntry(base.Source, logText, logType);
            }
            catch (Exception ex)
            {
                if (base.ThrowError)
                {
                    throw;
                }
                else
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}
