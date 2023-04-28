using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Extensions;
using System.Data;

namespace GBM_Stocks_Orders_Domain.Request
{
    /// <summary>
    /// Create order.
    /// </summary>
    public partial class CreateOrderRequest : StoredRequest
    {
        /// <summary>
        /// Issuer Name
        /// </summary>
        public string IssuerName { get; set; }

        /// <summary>
        /// Shares
        /// </summary>
        public int Shares { get; set; }

        /// <summary>
        /// Share Price
        /// </summary>
        public decimal SharePrice { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        public int Timestamp { get; set; }

        /// <summary>
        /// The account id
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Operation 1 Buy, 0 Sell
        /// </summary>
        public bool Operation { get; set; }
    }

    public partial class CreateOrderRequest : StoredRequest
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
            return "usp_CreateOrder";
        }
    }
}
