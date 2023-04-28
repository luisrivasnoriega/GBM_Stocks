using GBM_Stocks_Accounts_Domain.Request;
using GBM_Stocks_Accounts_Infrastructure.Interfaces;
using GBM_Stocks_Core_Infrastructure.Interfaces;
using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Interfaces;
using GBM_Stocks_Orders_Core.Interfaces;
using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Views;

namespace GBM_Stocks_Orders_Core.Services
{
    public class OrdersService : IOrdersService
    {
        IAccountRepository AccountRepository { get; set; }
        IOrdersRepository OrderRepository { get; set; }
        IOrderBusinessRules OrderBusinessRules { get; set; }
        IUnitOfWork UnitOfWork { get; set; }

        public OrdersService(IOrdersRepository orderRepository, IOrderBusinessRules orderBusinessRules, IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            OrderRepository = orderRepository;
            OrderBusinessRules = orderBusinessRules;
            AccountRepository = accountRepository;
        }

        public async Task<StoredSingleResponse<CurrentBalanceView>> CreateOrder(CreateOrderRequest createOrderRequest, string successMessage)
        {
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
                        businessRule = OrderBusinessRules.DuplicatedOperation(details.Response.Orders, createOrderRequest);
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
                                    var share = details.Response.ShareByAccount.First(x => x.IssuerName == createOrderRequest.IssuerName);
                                    var updateShareByAccountRequest = OrderBusinessRules.UpdateShareByAccountRequest(createOrderRequest, share);
                                    await OrderRepository.UpdateShareByAccount(updateShareByAccountRequest, "Updating share by account");
                                }
                                else
                                {
                                    var createShareByAccountRequest = OrderBusinessRules.CreateShareByAccountRequest(createOrderRequest);
                                    await OrderRepository.CreateShareByAccount(createShareByAccountRequest, "Creating share by account");
                                }

                                var updateAccountRequest = new UpdateAccountRequest();
                                updateAccountRequest.AccountId = createOrderRequest.AccountId;
                                updateAccountRequest.Cash = OrderBusinessRules.UpdateCashAccount(createOrderRequest, details.Response.Account);

                                await AccountRepository.UpdateAccount(updateAccountRequest, "update account");
                                await OrderRepository.CreateOrder(createOrderRequest, successMessage);
                                await UnitOfWork.CommitAsync();
                            }
                        }
                    }
                }
            }

            details = await OrderRepository.GetAccountDetails(getAccountDetailsRequest, successMessage);
            var response = new StoredSingleResponse<CurrentBalanceView>();
            var currentBalanceView = new CurrentBalanceView();
            currentBalanceView.BusinessError = businessRule;

            if (string.IsNullOrEmpty(businessRule))
            {
                currentBalanceView.Issuers = details.Response.ShareByAccount.Where(x=> x.TotalShare >0).ToList();
                response.Success = true;
                response.Message = successMessage;
                currentBalanceView.Cash = details.Response.Account.Cash;
            }

            response.Response = currentBalanceView;
            return response;
        }
    }
}
