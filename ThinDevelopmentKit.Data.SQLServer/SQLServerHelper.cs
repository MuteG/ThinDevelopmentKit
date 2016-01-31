using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ThinDevelopmentKit.Config;

namespace ThinDevelopmentKit.Data.SQLServer
{
	/// <summary>
    /// SQLServer操作対象
	/// </summary>
	public class SQLServerHelper : AbstractDBHelper
	{
        protected override string GenerateConnectionString(DBConfig config)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = config.DataSource;
            builder.InitialCatalog = config.DatabaseName;
            builder.UserID = config.UserID;
            builder.Password = config.Password;
            return builder.ConnectionString;
        }

        public override void TestConnection()
        {
            using (SqlConnection conn = new SqlConnection(base.ConnectionString))
            {
                conn.Open();
            }
        }

		public override int ExecuteNonQuery(CommandType commandType, string commandText, DbParameter[] commandParameters)
		{
            using (SqlConnection conn = new SqlConnection(base.ConnectionString))
		    {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
		        {
                    cmd.Connection = conn;
                    cmd.CommandType = commandType;
                    cmd.CommandText = commandText;
                    cmd.CommandTimeout = this.TimeoutSeconds;
                    if (null != commandParameters)
                    {
                        foreach (DbParameter param in commandParameters)
                        {
                            SqlParameter sqliteParameter = new SqlParameter();
                            sqliteParameter.DbType = param.DbType;
                            sqliteParameter.Direction = param.Direction;
                            sqliteParameter.Size = param.Size;
                            sqliteParameter.ParameterName = param.ParameterName;
                            sqliteParameter.Value = param.Value;

                            cmd.Parameters.Add(sqliteParameter);
                        }
                    }
                    return cmd.ExecuteNonQuery();
		        }
		    }
		}


        public override IDataReader ExecuteReader(CommandType commandType, string commandText, DbParameter[] commandParameters)
		{
            SqlConnection conn = new SqlConnection(base.ConnectionString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = commandType;
                cmd.CommandText = commandText;
                cmd.CommandTimeout = this.TimeoutSeconds;
                if (null != commandParameters)
                {
                    foreach (DbParameter param in commandParameters)
                    {
                        SqlParameter sqliteParameter = new SqlParameter();
                        sqliteParameter.DbType = param.DbType;
                        sqliteParameter.Direction = param.Direction;
                        sqliteParameter.Size = param.Size;
                        sqliteParameter.ParameterName = param.ParameterName;
                        sqliteParameter.Value = param.Value;

                        cmd.Parameters.Add(sqliteParameter);
                    }
                }
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
		}

        public override DbParameter GenerateParameter(string parameterName, DbType dataType, int size, object value)
        {
            return new SqlParameter()
            {
                ParameterName = parameterName,
                DbType = dataType,
                Size = size,
                Value = value
            };
        }
    }
}
