using GBM_Stocks_Database;
using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Interfaces;

namespace GBM_Stocks_Infrastructure.Implementations
{
    /// <summary>
    /// </summary>
    public class Transact : ITransact
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="exceptionManager"></param>
        public Transact(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMap"></typeparam>
        /// <param name="stored"></param>
        /// <param name="successMessage"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public async Task<StoredResponse<TMap>> Execution<T, TMap>(T stored, string successMessage, string errorMessage) where T : StoredRequest where TMap : class
        {
            var storedResponse = new StoredResponse<TMap>();
            var response = await stored.ExecDataSetAsync<TMap>(UnitOfWork.Connection, UnitOfWork.SqlTransaction);

            storedResponse.Success = true;
            storedResponse.Message = successMessage;
            storedResponse.Response = response;

            return storedResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stored"></param>
        /// <param name="successMessage"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public async Task<StoredResponse> MultipleExecution<T>(T stored, string successMessage, string errorMessage) where T : StoredRequest
        {
            var storedResponse = new StoredResponse();
            var response = await stored.ExecDataSetAsync(UnitOfWork.Connection, UnitOfWork.SqlTransaction);

            storedResponse.Success = true;
            storedResponse.Message = successMessage;
            storedResponse.Response = response;

            return storedResponse;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMap"></typeparam>
        /// <param name="stored"></param>
        /// <param name="successMessage"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public async Task<StoredSingleResponse<TMap>> SingleExecution<T, TMap>(T stored, string successMessage, string errorMessage) where T : StoredRequest where TMap : class
        {
            var storedResponse = new StoredSingleResponse<TMap>();
            var response = await stored.ExecAsync<TMap>(UnitOfWork.Connection, UnitOfWork.SqlTransaction);

            storedResponse.Success = true;
            storedResponse.Message = successMessage;
            storedResponse.Response = response;

            return storedResponse;
        }
    }
}