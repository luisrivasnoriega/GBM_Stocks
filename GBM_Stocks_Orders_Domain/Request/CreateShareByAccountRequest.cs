using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Extensions;
using System.Data;

namespace GBM_Stocks_Orders_Domain.Request
{
    public partial class CreateShareByAccountRequest : StoredRequest
    {
        /// <summary>
        /// The account id
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Issuer Name
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// Share Price
        /// </summary>
        public decimal SharePrice { get; set; }

        /// <summary>
        /// Total Share
        /// </summary>
        public int  TotalShare { get; set; }
    }

    public partial class CreateShareByAccountRequest : StoredRequest
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
            return "usp_CreateShareByAccount";
        }
    }
}
