using System.Data;
using System.Data.SqlClient;

namespace DapperSample.Storage
{
    public class SqlConnectionProvider
    {
        public IDbConnection GetNewConnection => new SqlConnection(Constants.SqlConnectionString);
    }
}