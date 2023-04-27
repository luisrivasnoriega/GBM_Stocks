namespace GBM_Stocks_Database.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PreValidationException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public PreValidationException() : base()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public PreValidationException(string message)
            : base(message)
        {

        }
    }
}