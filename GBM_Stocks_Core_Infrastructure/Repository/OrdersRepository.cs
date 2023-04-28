using GBM_Stocks_Core_Infrastructure.Interfaces;
using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Interfaces;
using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Response;

namespace GBM_Stocks_Core_Infrastructure.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        public ITransact Transact { get; set; }

        public OrdersRepository(ITransact transact)
        {
            Transact = transact;
        }

        public async Task<StoredSingleResponse<CreateOrderResponse>> CreateOrder(CreateOrderRequest createOrderRequest, string successMessage)
        {
            var getOrder = await Transact.SingleExecution<CreateOrderRequest, CreateOrderResponse>(createOrderRequest, successMessage);
            return getOrder;
        }
    }
}
