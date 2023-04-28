using GBM_Stocks_Core_Infrastructure.Interfaces;
using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Interfaces;
using GBM_Stocks_Orders_Core.Interfaces;
using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Response;

namespace GBM_Stocks_Orders_Core.Services
{
    public class OrdersService : IOrdersService
    {
        IOrdersRepository OrderRepository { get; set; }
        IOrderBusinessRules OrderBusinessRules { get; set; }
        IUnitOfWork UnitOfWork { get; set; }

        public OrdersService(IOrdersRepository orderRepository, IOrderBusinessRules orderBusinessRules, IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            OrderRepository = orderRepository;
            OrderBusinessRules = orderBusinessRules;
        }

        public async Task<StoredSingleResponse<CreateOrderResponse>> CreateOrder(CreateOrderRequest createOrderRequest, string successMessage)
        {
            StoredSingleResponse<CreateOrderResponse> response = new StoredSingleResponse<CreateOrderResponse>();

            var getAccountDetailsRequest = new GetAccountDetailsRequest();
            getAccountDetailsRequest.AccountId = createOrderRequest.AccountId;

            var details = await OrderRepository.GetAccountDetails(getAccountDetailsRequest, successMessage);
            string businessRule = string.Empty;

            businessRule = OrderBusinessRules.AccountExist(details.Response.Account);
            if (string.IsNullOrEmpty(businessRule))
            {
                businessRule = OrderBusinessRules.ClosedMarket(createOrderRequest.Timestamp);
                if (string.IsNullOrEmpty(businessRule))
                {
                    businessRule = OrderBusinessRules.MinimiumShare(createOrderRequest.Shares);
                    if (string.IsNullOrEmpty(businessRule))
                    {
                        var lastOrder = details.Response.Orders.MaxBy(x => x.Timestamp);
                        businessRule = OrderBusinessRules.DuplicatedOperation(lastOrder, createOrderRequest);
                        if (string.IsNullOrEmpty(businessRule))
                        {
                            //buy
                            if (createOrderRequest.Operation)
                            {
                                businessRule = OrderBusinessRules.InsufficientBalance(details.Response.Account.Cash, createOrderRequest);
                                if (string.IsNullOrEmpty(businessRule))
                                {
                                    response = await OrderRepository.CreateOrder(createOrderRequest, successMessage);
                                    await UnitOfWork.CommitAsync();
                                }
                            }
                            //sell
                            else
                            {

                            }
                        }
                    }
                }
            }

       

            response.Message = businessRule;
            return response;
        }
    }
}
