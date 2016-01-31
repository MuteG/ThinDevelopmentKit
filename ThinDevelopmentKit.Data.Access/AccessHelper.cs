using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using ThinDevelopmentKit.Config;

namespace ThinDevelopmentKit.Data.Access
{
    public class AccessHelper : AbstractDBHelper
    {
        public AccessHelper(DBConfig config)
            : base(config)
        {

        }

        public override DbParameter GenerateParameter(string parameterName, DbType dataType, int size, object value)
        {
            return new OleDbParameter()
            {
                ParameterName = parameterName,
                DbType = dataType,
                Size = size,
                Value = value
            };
        }

        protected override DbConnection CreateConnection(string conn)
        {
            return new OleDbConnection(conn);
        }

        protected override string GenerateConnectionString(DBConfig config)
        {
            OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            builder.DataSource = config.DataSource;
            builder.Provider = "Microsoft.Jet.OLEDB.4.0";
            return builder.ConnectionString;
        }
    }
}
