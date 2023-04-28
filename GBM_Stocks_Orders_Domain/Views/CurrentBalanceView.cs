
namespace GBM_Stocks_Orders_Domain.Views
{
    public class CurrentBalanceView
    {
        public decimal Cash { get; set; }
        public List<ShareByAccountView> Issuers { get; set; }
        public string BusinessError { get; set; }
    }
}
