using GBM_Stocks_Database.Domain;
using GBM_Stocks_Database.Exceptions;
using GBM_Stocks_Database.Interfaces;
using System.Data;

namespace GBM_Stocks_Database.Implementantions
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class StoredExecutionFlow : IStoredExecutionFlow
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <param name="db"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public async Task<List<string>> ExecutionMethodDataSetAsync(StoredRequest request, IDbConnection db, IDbTransaction dbTransaction)
        {
            List<string> records;
            if (await request.PreValidate())
                if (await request.Validate(db))
                {
                    records = await StoredExecution.InternalExecDataSetAsync(db, request, dbTransaction);
                    request.PostOperate(db);
                }
                else
                {
                    throw new ValidationException();
                }
            else
                throw new PreValidationException();

            return records;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="db"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecutionMethodDataSetAsync<T>(StoredRequest request, IDbConnection db, IDbTransaction dbTransaction)
        {
            IEnumerable<T> records;
            if (await request.PreValidate())
                if (await request.Validate(db))
                {
                    records = await StoredExecution.InternalExecDataSetAsync<T>(db, request, dbTransaction);
                    request.PostOperate(db);
                }
                else
                {
                    throw new ValidationException();
                }
            else
                throw new PreValidationException();
            return records;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="db"></param>
        /// <param name="dbTransaction"></param>
        /// <returns></returns>
        public async Task<T> ExecutionMethodAsync<T>(StoredRequest request, IDbConnection db, IDbTransaction dbTransaction)
        {
            T record;
            if (await request.PreValidate())
                if (await request.Validate(db))
                {
                    record = await StoredExecution.InternalExecAsync<T>(db, request, dbTransaction);
                    request.PostOperate(db);
                }
                else
                {
                    throw new ValidationException();
                }
            else
                throw new PreValidationException();
            return record;
        }
    }
}