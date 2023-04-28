using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Response;

namespace GBM_Stocks_Orders_Core.Interfaces
{
    public interface IOrderBusinessRules
    {
        public string MinimiumShare(int share);
        public string AccountExist(AccountView account);
        public string ClosedMarket(int timestamp);
        public string DuplicatedOperation(OrderView? order, CreateOrderRequest createOrderRequest);
        public string InsufficientBalance(decimal cash, CreateOrderRequest createOrderRequest);
        public string InsufficientStocks(int share, CreateOrderRequest createOrderRequest);
    }
}
