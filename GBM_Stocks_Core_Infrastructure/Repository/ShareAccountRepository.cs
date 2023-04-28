
using GBM_Stocks_Core_Infrastructure.Interfaces;
using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Interfaces;
using GBM_Stocks_Orders_Domain.Request;
using GBM_Stocks_Orders_Domain.Response;

namespace GBM_Stocks_Core_Infrastructure.Repository
{
    public class ShareAccountRepository : IShareAccountRepository
    {
        public ITransact Transact { get; set; }

        public ShareAccountRepository(ITransact transact)
        {
            Transact = transact;
        }

        public async Task<StoredSingleResponse<GetShareByAccountResponse>> GetShareByAccount(GetShareByAccountRequest getShareByAccountRequest, string successMessage)
        {
            var getShareByAccount = await Transact.SingleExecution<GetShareByAccountRequest, GetShareByAccountResponse>(getShareByAccountRequest, successMessage);
            return getShareByAccount;
        }
    }
}
