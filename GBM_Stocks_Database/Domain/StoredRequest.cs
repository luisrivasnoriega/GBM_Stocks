using System.Data;

namespace GBM_Stocks_Database.Domain
{
    /// <summary>
    ///     Abstract class to standarize the execution process of a stored procedure
    /// </summary>
    public abstract class StoredRequest
    {
        /// <summary>
        ///     This method will be the first to be executed
        /// </summary>
        /// <returns>
        /// </returns>
        public abstract Task<bool> PreValidate();

        /// <summary>
        ///     This method will only run if the method of prevalidate was executed correctly and returned a positive result. You
        ///     can perform a validation process with the database
        /// </summary>
        /// <param name="db">Database connection</param>
        /// <returns></returns>
        public abstract Task<bool> Validate(IDbConnection db);

        /// <summary>
        ///     This method will only run if the main action was executed correctly.
        ///     You can perform a process with the database
        /// </summary>
        /// <param name="db">Database connection</param>
        public abstract Task PostOperate(IDbConnection db);

        /// <summary>
        ///     Set the name of the stored procedure
        /// </summary>
        /// <returns>
        ///     Gets the name of the stored procedure
        /// </returns>
        public abstract string GetStoredName();
    }
}
