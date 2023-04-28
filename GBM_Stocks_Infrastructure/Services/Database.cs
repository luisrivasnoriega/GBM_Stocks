using System.Data.Common;
using System.Data.SqlClient;

namespace GBM_Stocks_Infrastructure.Services
{
    internal class Database
    {
        public string GetConnectionString()
        {
            var connectionStringBuilder = new DbConnectionStringBuilder();

            connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "CENTIMX\\CENTIMX_SQL",
                UserID = "sa",
                Password = "SaMaster",
                InitialCatalog = "GBM",
                TrustServerCertificate = true
            };

            return connectionStringBuilder.ConnectionString;
        }
    }
}
