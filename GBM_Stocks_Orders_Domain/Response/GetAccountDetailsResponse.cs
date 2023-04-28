namespace GBM_Stocks_Orders_Domain.Response
{
    public partial class GetAccountDetailsResponse
    {
        public List<OrderView> Orders { get; set; }
        public List<ShareByAccountView> ShareByAccount { get; set; }
        public AccountView Account { get; set; }
    }

    public class OrderView
    {
        public int OrderId { get; set; }
        public string IssuerName { get; set; }
        public int Shares { get; set; }
        public decimal SharePrice { get; set; }
        public int Timestamp { get; set; }
        public int AccountId { get; set; }
        public bool Operation { get; set; }
    }

    public class ShareByAccountView
    {
        public string IssuerName { get; set; }
        public decimal SharePrice { get; set; }
        public int TotalShare { get; set; }
    }

    public class AccountView
    {
        public int AccountId { get; set; }
        public decimal Cash { get; set; }
    }
}
