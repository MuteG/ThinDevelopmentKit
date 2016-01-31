using System;
using ThinDevelopmentKit.Data;

namespace ThinDevelopmentKit.Config
{
    /// <summary>
    /// 数据库设定
    /// </summary>
    [Serializable]
    public class DBConfig
    {
        /// <summary>
        /// 获取或设置数据库种类
        /// </summary>
        public DatabaseType DBType { get; set; }

        /// <summary>
        /// 获取或设置数据库源
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 获取或设置数据库名称
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// 获取或设置用户名
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 获取或设置登陆口令
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 数据库操作超时时间
        /// <para>秒单位</para>
        /// </summary>
        public int TimeoutSeconds { get; set; }

        public DBConfig()
        {
            this.DBType = DatabaseType.SQLServer;
            this.DataSource = "localhost";
            this.DatabaseName = "master";
            this.UserID = "sa";
            this.Password = "infact";
            this.TimeoutSeconds = 30;
        }
    }
}
