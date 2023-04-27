namespace GBM_Stocks_Database.Domain
{
    /// <summary>
    /// Class that defines a response from a stored procedure.
    /// </summary>
    public class StoredResponse : Response
    {
        /// <summary>
        /// List containing the response.
        /// </summary>
        public List<string>? Response { get; set; }
    }

    /// <summary>
    /// Clase que define una respuesta de un stored procedure.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StoredResponse<T> : Response
    {
        /// <summary>
        /// List containing the response.
        /// </summary>
        public IEnumerable<T>? Response { get; set; }
    }
}