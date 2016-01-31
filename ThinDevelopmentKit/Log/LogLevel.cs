namespace ThinDevelopmentKit.Log
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1 << 0,
        /// <summary>
        /// 调试信息
        /// </summary>
        Debug = 1 << 1,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = 1 << 2,
        /// <summary>
        /// 异常
        /// </summary>
        Error = 1 << 3
    }
}
