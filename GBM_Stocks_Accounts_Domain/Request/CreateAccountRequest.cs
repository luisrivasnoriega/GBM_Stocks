using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Extensions;
using System.Data;

namespace GBM_Stocks_Accounts_Domain.Request
{
    /// <summary>
    /// Create account.
    /// </summary>
    public partial class CreateAccountRequest : StoredRequest
    {
        /// <summary>
        /// Cash
        /// </summary>
        public decimal Cash { get; set; }
    }

    public partial class CreateAccountRequest : StoredRequest
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
            return "usp_CreateAccount";
        }
    }
}
