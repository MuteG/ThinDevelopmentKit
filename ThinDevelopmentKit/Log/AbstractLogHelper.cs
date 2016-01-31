namespace ThinDevelopmentKit.Log
{
    /// <summary>
    /// 抽象的日志助手对象，不能直接实例化使用
    /// </summary>
    abstract class AbstractLogHelper : ILogHelper
    {
        #region ILogHelper 成员

        /// <summary>
        /// 获取或设置日志输出位置
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="level">日志等级</param>
        /// <param name="message">日志信息</param>
        public abstract void WriteLog(LogLevel level, string message);

        #endregion

        /// <summary>
        /// 输出日志的过程中发生异常时是否向外抛出
        /// </summary>
        public bool ThrowError { get; set; }

        protected string GetLevelText(LogLevel level)
        {
            string levelText = string.Empty;
            switch (level)
            {
                case LogLevel.Normal:
                    levelText = "[正常]";
                    break;
                case LogLevel.Warning:
                    levelText = "[警告]";
                    break;
                case LogLevel.Debug:
                    levelText = "[调试]";
                    break;
                case LogLevel.Error:
                    levelText = "[异常]";
                    break;
            }
            return levelText;
        }

        public override string ToString()
        {
            return Source;
        }

        public override int GetHashCode()
        {
            return this.Source.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ILogHelper loger = obj as ILogHelper;
            if (null == loger)
            {
                return false;
            }
            else
            {
                return this.Source.Equals(loger.Source);
            }
        }
    }
}
