using GBM_Stocks_Database.Domain;
using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Views;

namespace GBM_Stocks_Orders_Core.Interfaces
{
    public interface IOrdersService
    {
        public Task<StoredSingleResponse<CurrentBalanceView>> CreateOrder(CreateOrderRequest createOrderRequest, string successMessage);
    }
}
