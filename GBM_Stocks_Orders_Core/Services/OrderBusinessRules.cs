using GBM_Stocks_Orders_Core.Extensions;
using GBM_Stocks_Orders_Core.Interfaces;
using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Views;

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

        public string DuplicatedOperation(List<OrderView> orders, CreateOrderRequest createOrderRequest)
        {
            if (orders.Any(x => (x.Timestamp.UnixTimeStampToDateTime() - createOrderRequest.Timestamp.UnixTimeStampToDateTime()).TotalMinutes < 5 && x.SharePrice == createOrderRequest.SharePrice && x.Shares == createOrderRequest.Shares && x.IssuerName == createOrderRequest.IssuerName))
            {
                return "This operation is duplicated";
            }
            return string.Empty;
        }

        public string InsufficientBalance(decimal cash, CreateOrderRequest createOrderRequest)
        {
            decimal total = createOrderRequest.SharePrice * createOrderRequest.Shares;
            return cash < total ? "Cash is not enough" : "";
        }

        public string InsufficientStocks(ShareByAccountView? share, CreateOrderRequest createOrderRequest)
        {
            if (share != null && share.TotalShare < createOrderRequest.Shares)
            {
                return "Insufficient Stocks";
            }
            return string.Empty;
        }

        public string MinimiumShare(int share)
        {
            return share < 0 ? "You must put a share amount greater than 0" : string.Empty;
        }

        public UpdateShareByAccountRequest UpdateShareByAccountRequest(CreateOrderRequest createOrderRequest, ShareByAccountView share)
        {
            decimal total;
            if (createOrderRequest.Operation)
                total = ((share.SharePrice * share.TotalShare) + (createOrderRequest.SharePrice * createOrderRequest.Shares)) / (share.TotalShare + createOrderRequest.Shares);
            else
            {
                var remainingStocks = (share.TotalShare - createOrderRequest.Shares);
                if (remainingStocks == 0)
                    total = 0;
                else
                    total = ((share.SharePrice * share.TotalShare) - (createOrderRequest.SharePrice * createOrderRequest.Shares)) / remainingStocks;
            }

            var updateShareByAccountRequest = new UpdateShareByAccountRequest();
            updateShareByAccountRequest.AccountId = createOrderRequest.AccountId;
            updateShareByAccountRequest.SharePrice = total;
            updateShareByAccountRequest.IssuerName = createOrderRequest.IssuerName;

            if (createOrderRequest.Operation)
                updateShareByAccountRequest.TotalShare = share.TotalShare + createOrderRequest.Shares;
            else
                updateShareByAccountRequest.TotalShare = share.TotalShare - createOrderRequest.Shares;

            return updateShareByAccountRequest;
        }

        public CreateShareByAccountRequest CreateShareByAccountRequest(CreateOrderRequest createOrderRequest)
        {
            var createShareByAccountRequest = new CreateShareByAccountRequest
            {
                AccountId = createOrderRequest.AccountId,
                SharePrice = createOrderRequest.SharePrice,
                IssuerName = createOrderRequest.IssuerName,
                TotalShare = createOrderRequest.Shares
            };

            return createShareByAccountRequest;
        }

        public decimal UpdateCashAccount(CreateOrderRequest createOrderRequest, AccountView accountView)
        {
            decimal total = createOrderRequest.Operation
                ? accountView.Cash - (createOrderRequest.SharePrice * createOrderRequest.Shares)
                : accountView.Cash + (createOrderRequest.SharePrice * createOrderRequest.Shares);
            return total;
        }
    }
}
