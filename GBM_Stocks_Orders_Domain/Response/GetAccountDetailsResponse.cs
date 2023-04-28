using GBM_Stocks_Orders_Domain.Views;

namespace GBM_Stocks_Orders_Domain.Response
{
    public partial class GetAccountDetailsResponse
    {
        public List<OrderView> Orders { get; set; }
        public List<ShareByAccountView> ShareByAccount { get; set; }
        public AccountView Account { get; set; }
    }
}
