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
            var response = new StoredSingleResponse<CreateOrderResponse>();
            var getAccountDetailsRequest = new GetAccountDetailsRequest
            {
                AccountId = createOrderRequest.AccountId
            };
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
                            }
                            //sell
                            else
                            {
                                var actualShare = details.Response.ShareByAccount.FirstOrDefault(x => x.IssuerName == createOrderRequest.IssuerName);
                                businessRule = OrderBusinessRules.InsufficientStocks(actualShare, createOrderRequest);
                            }

                            if (string.IsNullOrEmpty(businessRule))
                            {

                                if (details.Response.ShareByAccount.Any(x => x.IssuerName == createOrderRequest.IssuerName))
                                {
                                    //update
                                    decimal total;

                                    var share = details.Response.ShareByAccount.First(x => x.IssuerName == createOrderRequest.IssuerName);
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

                                    await OrderRepository.UpdateShareByAccount(updateShareByAccountRequest, "");
                                }
                                else
                                {
                                    //add
                                    var createShareByAccountRequest = new CreateShareByAccountRequest();

                                    createShareByAccountRequest.AccountId = createOrderRequest.AccountId;
                                    createShareByAccountRequest.SharePrice = createOrderRequest.SharePrice;
                                    createShareByAccountRequest.IssuerName = createOrderRequest.IssuerName;
                                    createShareByAccountRequest.TotalShare = createOrderRequest.Shares;

                                    await OrderRepository.CreateShareByAccount(createShareByAccountRequest, "");
                                }

                                response = await OrderRepository.CreateOrder(createOrderRequest, successMessage);
                                await UnitOfWork.CommitAsync();
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
