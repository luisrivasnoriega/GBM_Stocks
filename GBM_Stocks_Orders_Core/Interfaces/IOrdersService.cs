using GBM_Stocks_Database.Domain;
using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Response;

namespace GBM_Stocks_Orders_Core.Interfaces
{
    public interface IOrdersService
    {
        public Task<StoredSingleResponse<CreateOrderResponse>> CreateOrder(CreateOrderRequest createOrderRequest, string successMessage);
    }
}
