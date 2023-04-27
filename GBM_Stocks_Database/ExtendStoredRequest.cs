using GBM_Stocks_Database.Domain;
using GBM_Stocks_Database.Implementantions;
using GBM_Stocks_Database.Interfaces;
using System.Data;

namespace GBM_Stocks_Database
{
    /// <summary>
    ///     Extension methods to execute stored procedures
    /// </summary>
    public static class ExtendStoredRequest
    {
        /// <summary>
        /// </summary>
        /// <param name="request">An object that inherits from the StoredRequest class.</param>
        /// <param name="connection">Database connection.</param>
        /// <param name="dbTransaction">Database transaction scope</param>
        /// <param name="storedExecutionFlow">
        ///     A specific workflow to run the procedure. It can be null, since Drapper already
        ///     contains the own.
        /// </param>
        /// <returns></returns>
        public static async Task<List<string>> ExecDataSetAsync(this StoredRequest request, IDbConnection connection, IDbTransaction dbTransaction, IStoredExecutionFlow? storedExecutionFlow = null)
        {
            storedExecutionFlow ??= new StoredExecutionFlow();
            return await storedExecutionFlow.ExecutionMethodDataSetAsync(request, connection, dbTransaction);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">An object that inherits from the StoredRequest class.</param>
        /// <param name="connection">Database connection.</param>
        /// <param name="dbTransaction">Database transaction scope</param>
        /// <param name="storedExecutionFlow">
        ///     A specific workflow to run the procedure. It can be null, since Drapper already
        ///     contains the own.
        /// </param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ExecDataSetAsync<T>(this StoredRequest request, IDbConnection connection, IDbTransaction dbTransaction, IStoredExecutionFlow? storedExecutionFlow = null)
        {
            storedExecutionFlow ??= new StoredExecutionFlow();
            return await storedExecutionFlow.ExecutionMethodDataSetAsync<T>(request, connection, dbTransaction);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request">An object that inherits from the StoredRequest class.</param>
        /// <param name="connection">Database connection.</param>
        /// <param name="dbTransaction">Database transaction scope</param>
        /// <param name="storedExecutionFlow">
        ///     A specific workflow to run the procedure. It can be null, since Drapper already
        ///     contains the own.
        /// </param>
        /// <returns></returns>
        public static async Task<T> ExecAsync<T>(this StoredRequest request, IDbConnection connection, IDbTransaction dbTransaction, IStoredExecutionFlow? storedExecutionFlow = null)
        {
            storedExecutionFlow ??= new StoredExecutionFlow();
            return await storedExecutionFlow.ExecutionMethodAsync<T>(request, connection, dbTransaction);
        }
    }
}