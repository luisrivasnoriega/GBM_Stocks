using GBM_Stocks_Database.Domain;

namespace GBM_Stocks_Infrastructure.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransact
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMap"></typeparam>
        /// <param name="stored"></param>
        /// <param name="successMessage"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        Task<StoredResponse<TMap>> Execution<T, TMap>(T stored, string successMessage) where T : StoredRequest where TMap : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMap"></typeparam>
        /// <param name="stored"></param>
        /// <param name="successMessage"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        Task<StoredSingleResponse<TMap>> SingleExecution<T, TMap>(T stored, string successMessage) where T : StoredRequest where TMap : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stored"></param>
        /// <param name="successMessage"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        Task<StoredResponse> MultipleExecution<T>(T stored, string successMessage) where T : StoredRequest;
    }
}