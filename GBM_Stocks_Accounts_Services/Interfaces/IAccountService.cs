using GBM_Stocks_Accounts_Domain.Request;
using GBM_Stocks_Accounts_Domain.Response;
using GBM_Stocks_Database.Domain;

namespace GBM_Stocks_Accounts_Core.Interfaces
{
    public interface IAccountService
    {
        public Task<StoredSingleResponse<GetAccountResponse>> GetAccountById(GetAccountRequest getAccountRequest, string successMessage);
        public Task<StoredSingleResponse<GetAccountResponse>> CreateAccount(CreateAccountRequest createAccountRequest, string successMessage);
    }
}
