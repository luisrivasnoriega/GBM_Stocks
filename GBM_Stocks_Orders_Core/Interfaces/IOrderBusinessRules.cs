using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Response;
using GBM_Stocks_Orders_Domain.Views;

namespace GBM_Stocks_Orders_Core.Interfaces
{
    public interface IOrderBusinessRules
    {
        public string MinimiumShare(int share);
        public string AccountExist(AccountView account);
        public string ClosedMarket(int timestamp);
        public string DuplicatedOperation(List<OrderView> order, CreateOrderRequest createOrderRequest);
        public string InsufficientBalance(decimal cash, CreateOrderRequest createOrderRequest);
        public string InsufficientStocks(ShareByAccountView? share, CreateOrderRequest createOrderRequest);


        public UpdateShareByAccountRequest UpdateShareByAccountRequest(CreateOrderRequest createOrderRequest, ShareByAccountView share);
        public CreateShareByAccountRequest CreateShareByAccountRequest(CreateOrderRequest createOrderRequest);
        public decimal UpdateCashAccount(CreateOrderRequest createOrderRequest, AccountView accountView);
    }
}
