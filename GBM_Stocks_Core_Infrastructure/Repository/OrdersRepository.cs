using GBM_Stocks_Core_Infrastructure.Interfaces;
using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Interfaces;
using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Response;
using GBM_Stocks_Orders_Domain.Views;
using System.Text.Json;

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

        public async Task<StoredSingleResponse<GetAccountDetailsResponse>> GetAccountDetails(GetAccountDetailsRequest getAccountDetailsRequest, string successMessage)
        {
            var response = new StoredSingleResponse<GetAccountDetailsResponse>();

            var getOrder = await Transact.MultipleExecution(getAccountDetailsRequest, successMessage);
            var getAccountDetailsResponse = new GetAccountDetailsResponse();

            if (getOrder.Success && getOrder.Response != null && getOrder.Response.Count == 3)
            {
                getAccountDetailsResponse.Orders = JsonSerializer.Deserialize<List<OrderView>>(getOrder.Response[0]);
                getAccountDetailsResponse.ShareByAccount = JsonSerializer.Deserialize<List<ShareByAccountView>>(getOrder.Response[1]);
                getAccountDetailsResponse.Account = JsonSerializer.Deserialize<List<AccountView>>(getOrder.Response[2]).FirstOrDefault();
            }

            response.Response = getAccountDetailsResponse;
            response.Success = getOrder.Success;
            response.Object = getOrder.Object;
            response.Message = getOrder.Message;

            return response;
        }

        public async Task<StoredSingleResponse<CreateShareByAccountResponse>> CreateShareByAccount(CreateShareByAccountRequest createShareByAccountRequest, string successMessage)
        {
            var createdShareByAccount = await Transact.SingleExecution<CreateShareByAccountRequest, CreateShareByAccountResponse>(createShareByAccountRequest, successMessage);
            return createdShareByAccount;
        }

        public async Task<StoredSingleResponse<UpdateShareByAccountResponse>> UpdateShareByAccount(UpdateShareByAccountRequest updateShareByAccountRequest, string successMessage)
        {
            var updatedShareByAccount = await Transact.SingleExecution<UpdateShareByAccountRequest, UpdateShareByAccountResponse>(updateShareByAccountRequest, successMessage);
            return updatedShareByAccount;
        }
    }
}
