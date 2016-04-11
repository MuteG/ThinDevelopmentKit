using System;
using System.Data;
using System.Data.Common;

namespace ThinDevelopmentKit.Data
{
	/// <summary>
    /// DB操作接口
	/// </summary>
	public interface IDBHelper : IDisposable
    {
        /// <summary>
        /// 测试数据库连接
        /// </summary>
        void TestConnection();

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTranscation();

        /// <summary>
        /// 执行指定的命令字符串
        /// </summary>
        /// <param name="sql">要执行的命令字符串</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>受影响的行数</returns>
		int ExecuteNonQuery(string sql, params DbParameter[] parameters);

        /// <summary>
        /// 执行指定的命令字符串，返回IDataReader。
        /// </summary>
        /// <param name="sql">要执行的命令字符串</param>
        /// <param name="commandParameters">参数</param>
        /// <returns></returns>
		DbDataReader ExecuteReader(string sql, params DbParameter[] parameters);

        T ExecuteScalar<T>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// 作成参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="size">数据大小</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        DbParameter GenerateParameter(string parameterName, DbType dataType, int size, object value);
	}
}
