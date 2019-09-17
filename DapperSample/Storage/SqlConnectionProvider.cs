using System.Data;
using System.Data.SqlClient;

namespace DapperDemo.Storage
{
    public class SqlConnectionProvider
    {
        public IDbConnection GetNewConnection => new SqlConnection(Constants.SqlConnectionString);
    }
}