using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using ThinDevelopmentKit.Config;

namespace ThinDevelopmentKit.Data.SQLite
{
    /// <summary>
    /// SQLite操作対象
    /// </summary>
    public class SQLiteHelper : AbstractDBHelper
    {
        public SQLiteHelper(DBConfig config)
            : base(config)
        {
        }

        protected override string GenerateConnectionString(DBConfig config)
        {
            SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
            builder.DataSource = config.DataSource;
            return builder.ConnectionString;
        }

        public override DbParameter GenerateParameter(string parameterName, DbType dataType, int size, object value)
        {
            return new SQLiteParameter()
            {
                ParameterName = parameterName,
                DbType = dataType,
                Size = size,
                Value = value
            };
        }

        protected override DbConnection CreateConnection(string conn)
        {
            return new SQLiteConnection(conn);
        }
    }
}
