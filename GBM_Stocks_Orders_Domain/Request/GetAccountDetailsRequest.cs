using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Extensions;
using System.Data;

namespace GBM_Stocks_Orders_Domain.Request
{
    /// <summary>
    /// Get all the details
    /// </summary>
    public partial class GetAccountDetailsRequest : StoredRequest
    {
       public int AccountId { get; set; }
    }

    public partial class GetAccountDetailsRequest : StoredRequest
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
            return "usp_GetAccountDetails";
        }
    }
}
