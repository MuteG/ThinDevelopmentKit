using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace ThinDevelopmentKit.Log
{
    /// <summary>
    /// 文本文件日志助手
    /// </summary>
    internal class TextLogHelper : AbstractLogHelper
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
                string exeName = Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location);
                string logFile = Path.Combine(this.Source,
                    string.Format("{1}_{0:yyyyMMdd}.log", DateTime.Now, exeName));
                string logText = string.Format(
                    "{0:yyyyMMdd HH:mm:ss.fff} {1}：{2}",
                    DateTime.Now, GetLevelText(level), message);
                using (FileStream stream = new FileStream(logFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Default))
                    {
                        writer.AutoFlush = true;
                        writer.WriteLine(logText);
                    }
                }
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
