namespace GBM_Stocks_Database.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ValidationException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidationException() : base()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ValidationException(string message) : base(message)
        {

        }
    }
}