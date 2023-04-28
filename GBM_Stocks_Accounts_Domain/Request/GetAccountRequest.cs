using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Extensions;
using System.Data;

namespace GBM_Stocks_Accounts_Domain.Request
{
    /// <summary>
    /// Get account by Id.
    /// </summary>
    public partial class GetAccountRequest : StoredRequest
    {
        /// <summary>
        /// Account Id.
        /// </summary>
        public int AccountId { get; set; }
    }

    public partial class GetAccountRequest : StoredRequest
    {
        public override Task<bool> PreValidate()
        {
            this.IsValid();
            return Task.FromResult(true);
        }

        public override Task<bool> Validate(IDbConnection db)
        {
            return Task.FromResult(true);
        }

        public override Task PostOperate(IDbConnection db)
        {
            return Task.CompletedTask;
        }

        public override string GetStoredName()
        {
            return "usp_GetAccount";
        }
    }
}
