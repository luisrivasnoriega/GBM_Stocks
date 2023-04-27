using Dapper;
using GBM_Stocks_Database.Domain;
using System.Data;
using System.Text.Json;

namespace GBM_Stocks_Database
{
    /// <summary>
    /// </summary>
    internal static class StoredExecution
    {
        /// <summary>
        /// </summary>
        /// <param name="db"></param>
        /// <param name="request"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        internal static async Task<List<string>> InternalExecDataSetAsync(IDbConnection db, StoredRequest request, IDbTransaction dbTransaction)
        {
            var records = new List<string>();
            using (var multi = await db.QueryMultipleAsync(request.GetStoredName(), request,
                commandType: CommandType.StoredProcedure, transaction: dbTransaction))
            {
                while (multi.IsConsumed == false)
                {
                    records.Add(JsonSerializer.Serialize(multi.Read().ToList()));
                }
            }
            return records;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="request"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        internal static async Task<IEnumerable<T>> InternalExecDataSetAsync<T>(IDbConnection db, StoredRequest request, IDbTransaction dbTransaction)
        {
            var record = await db.QueryAsync<T>(request.GetStoredName(), request, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
            return record;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="db"></param>
        /// <param name="request"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        internal static async Task<T> InternalExecAsync<T>(IDbConnection db, StoredRequest request, IDbTransaction dbTransaction)
        {
            var record = await db.QueryFirstOrDefaultAsync<T>(request.GetStoredName(), request, commandType: CommandType.StoredProcedure, transaction: dbTransaction);
            return record;
        }
    }
}