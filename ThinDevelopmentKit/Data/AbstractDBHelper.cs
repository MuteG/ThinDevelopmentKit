using System;
using System.Data;
using System.Data.Common;
using ThinDevelopmentKit.Config;

namespace ThinDevelopmentKit.Data
{
    public abstract class AbstractDBHelper : IDBHelper
    {
        protected DbConnection conn = null;
        private DbTransaction tran = null;
        /// <summary>
        /// 数据库操作超时时间
        /// <para>秒単位</para>
        /// </summary>
        private int timeoutSeconds = 180;

        public AbstractDBHelper(DBConfig config)
        {
            this.timeoutSeconds = config.TimeoutSeconds;
            string connectionString = this.GenerateConnectionString(config);
            this.conn = this.CreateConnection(connectionString);
        }

        protected abstract DbConnection CreateConnection(string conn);

        protected abstract string GenerateConnectionString(DBConfig config);

        public DbTransaction Transaction
        {
            get
            {
                return this.tran;
            }
            set
            {
                this.tran = value;
                this.conn = this.tran.Connection;
            }
        }

        public void BeginTransaction()
        {
            if (this.tran == null)
            {
                if (this.conn.State == ConnectionState.Broken || this.conn.State == ConnectionState.Closed)
                {
                    this.conn.Open();
                }
                this.tran = this.conn.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            if (this.tran != null)
            {
                this.tran.Commit();
                this.tran = null;
                this.tranCmd = null;
            }
        }

        public void RollbackTranscation()
        {
            if (this.tran != null)
            {
                this.tran.Rollback();
                this.tran = null;
                this.tranCmd = null;
            }
        }

        private DbCommand GenerateCommand(string sql, params DbParameter[] parameters)
        {
            if (this.conn.State == ConnectionState.Broken || this.conn.State == ConnectionState.Closed)
            {
                this.conn.Open();
            }
            DbCommand cmd = this.conn.CreateCommand();
            //if (this.tran != null)
            //{
            //    cmd.Transaction = this.tran;
            //}
            cmd.CommandTimeout = this.timeoutSeconds;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(parameters);
            return cmd;
        }

        #region IDBHelper 成员

        public void TestConnection()
        {
            this.conn.Open();
        }

        public abstract DbParameter GenerateParameter(string parameterName, DbType dataType, int size, object value);

        public DbDataReader ExecuteReader(string sql, params DbParameter[] parameters)
        {
            DbCommand cmd = this.GenerateCommand(sql, parameters);
            return cmd.ExecuteReader();
        }

        private DbCommand tranCmd = null;

        public int ExecuteNonQuery(string sql, params DbParameter[] parameters)
        {
            DbCommand cmd;
            if (this.tran != null)
            {
                if (tranCmd == null)
                {
                    tranCmd = this.GenerateCommand(sql);
                }
                cmd = tranCmd;
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(parameters);
            }
            else
            {
                cmd = this.GenerateCommand(sql, parameters);
            }
            return cmd.ExecuteNonQuery();
        }

        public T ExecuteScalar<T>(string sql, params DbParameter[] parameters)
        {
            DbCommand cmd = this.GenerateCommand(sql, parameters);
            return (T)Convert.ChangeType(cmd.ExecuteScalar(), typeof(T));
        }
        #endregion

        public void Dispose()
        {
            if (this.tran != null)
            {
                this.tran.Commit();
                this.tran = null;
            }
            if (this.conn != null)
            {
                this.conn.Close();
                this.conn.Dispose();
            }
        }
    }
}
