namespace GBM_Stocks_Database.Domain
{
    /// <summary>
    /// This class defines a generic response.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// True if the endpoint succeeded. False if a server error occurred.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// A descriptive message of the response.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// A generic object that contains the response from the endpoint.
        /// </summary>
        public object? Object { get; set; }
    }
}