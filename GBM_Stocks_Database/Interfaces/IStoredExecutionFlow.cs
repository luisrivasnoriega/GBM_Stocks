using GBM_Stocks_Database.Domain;
using System.Data;

namespace GBM_Stocks_Database.Interfaces
{
    /// <summary>
    /// </summary>
    public interface IStoredExecutionFlow
    {
        /// <summary>
        ///     Executes a method that returns a list of strings
        /// </summary>
        /// <param name="request"></param>
        /// <param name="db"></param> 
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task<List<string>> ExecutionMethodDataSetAsync(StoredRequest request, IDbConnection db, IDbTransaction dbTransaction);

        /// <summary>
        ///     Executes a method that return a list of objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="db"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> ExecutionMethodDataSetAsync<T>(StoredRequest request, IDbConnection db, IDbTransaction dbTransaction);

        /// <summary>
        ///     Executes a method that return a single object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="db"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        Task<T> ExecutionMethodAsync<T>(StoredRequest request, IDbConnection db, IDbTransaction dbTransaction);
    }
}