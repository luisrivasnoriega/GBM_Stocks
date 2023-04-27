using GBM_Stocks_Infrastructure.Interfaces;
using System.Data.SqlClient;
using System.Data;
using GBM_Stocks_Infrastructure.Services;

namespace GBM_Stocks_Infrastructure.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public SqlTransaction SqlTransaction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SqlConnection Connection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public UnitOfWork()
        {
            var database = new Database();
            var connectionString = database.GetConnectionString();

            if (!string.IsNullOrEmpty(connectionString))
            {
                Connection = new SqlConnection(connectionString);
                Connection.Open();
                SqlTransaction = Connection.BeginTransaction();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task CloseConnectionAsync()
        {
            await Connection.CloseAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task CommitAsync()
        {
            await SqlTransaction.CommitAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task RollbackAsync()
        {
            await SqlTransaction.RollbackAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (Connection != null && Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
            GC.SuppressFinalize(this);
        }
    }
}
