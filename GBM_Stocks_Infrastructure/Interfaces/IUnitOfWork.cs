using System.Data.SqlClient;

namespace GBM_Stocks_Infrastructure.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnitOfWork
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
        public Task CloseConnectionAsync();

        /// <summary>
        /// 
        /// </summary>
        public Task CommitAsync();

        /// <summary>
        /// 
        /// </summary>
        public Task RollbackAsync();
    }
}
