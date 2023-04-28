using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Interfaces;
using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Response;

namespace GBM_Stocks_Core_Infrastructure.Interfaces
{
    public interface IOrdersRepository
    {
        ITransact Transact { get; set; }

        public Task<StoredSingleResponse<CreateOrderResponse>> CreateOrder(CreateOrderRequest createOrderRequest, string successMessage);

        public Task<StoredSingleResponse<GetAccountDetailsResponse>> GetAccountDetails(GetAccountDetailsRequest getAccountDetailsRequest, string successMessage);

        public Task<StoredSingleResponse<CreateShareByAccountResponse>> CreateShareByAccount(CreateShareByAccountRequest createShareByAccountRequest, string successMessage);

        public Task<StoredSingleResponse<UpdateShareByAccountResponse>> UpdateShareByAccount(UpdateShareByAccountRequest updateShareByAccountRequest, string successMessage);
    }
}
