namespace ThinDevelopmentKit.Log
{
    /// <summary>
    /// 日志助手接口
    /// </summary>
    internal interface ILogHelper
    {
        /// <summary>
        /// 获取或设置日志输出位置
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="level">日志等级</param>
        /// <param name="message">日志信息</param>
        void WriteLog(LogLevel level, string message);
    }
}
