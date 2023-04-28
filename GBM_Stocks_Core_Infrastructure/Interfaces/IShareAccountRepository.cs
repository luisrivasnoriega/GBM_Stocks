using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Interfaces;
using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Response;

namespace GBM_Stocks_Core_Infrastructure.Interfaces
{
    public interface IShareAccountRepository
    {
        ITransact Transact { get; set; }

        public Task<StoredSingleResponse<GetShareByAccountResponse>> GetShareByAccount(GetShareByAccountRequest getShareByAccountRequest, string successMessage);
    }
}
