namespace GBM_Stocks_Database.Domain
{
    /// <summary>
    /// This class defines an error.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// This is a list containing the names of the error.
        /// </summary>
        public List<string>? MemberNames { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}
