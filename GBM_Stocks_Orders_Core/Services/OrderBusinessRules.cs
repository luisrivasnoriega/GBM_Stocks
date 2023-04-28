using GBM_Stocks_Orders_Core.Extensions;
using GBM_Stocks_Orders_Core.Interfaces;
using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Response;

namespace GBM_Stocks_Orders_Core.Services
{
    public class OrderBusinessRules : IOrderBusinessRules
    {
        public string AccountExist(AccountView account)
        {
            return account == null ? "Account not exist" : string.Empty;
        }

        public string ClosedMarket(int timestamp)
        {
            var actualTime = timestamp.UnixTimeStampToDateTime();
            return actualTime.Hour >= 6 && actualTime.Hour <= 15 ? string.Empty : "Market is closed";
        }

        public string DuplicatedOperation(OrderView? order, CreateOrderRequest createOrderRequest)
        {
            if (order != null)
            {
                if (order.SharePrice == createOrderRequest.SharePrice && order.Shares == createOrderRequest.Shares && order.IssuerName == createOrderRequest.IssuerName)
                {
                    var lastOrderTime = order.Timestamp.UnixTimeStampToDateTime();
                    var difference = createOrderRequest.Timestamp.UnixTimeStampToDateTime() - lastOrderTime;

                    if (difference.TotalMinutes < 5)
                    {
                        return "This operation is duplicated";
                    }
                }
            }
            return string.Empty;
        }

        public string InsufficientBalance(decimal cash, CreateOrderRequest createOrderRequest)
        {
            decimal total = createOrderRequest.SharePrice * createOrderRequest.Shares;
            return cash < total ? "Cash is insufficient" : "";
        }

        public string InsufficientStocks(int share, CreateOrderRequest createOrderRequest)
        {
            throw new NotImplementedException();
        }

        public string MinimiumShare(int share)
        {
            return share < 0 ? "You must put a share amount greater than 0" : string.Empty;
        }
    }
}
