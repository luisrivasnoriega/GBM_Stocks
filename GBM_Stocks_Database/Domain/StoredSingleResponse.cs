namespace GBM_Stocks_Database.Domain
{
    /// <summary>
    /// Class that defines a response from a stored procedure. Returns just one element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StoredSingleResponse<T> : Response
    {
        /// <summary>
        /// The response.
        /// </summary>
        public T? Response { get; set; }
    }
}