using GBM_Stocks_Accounts_Domain.Request;
using GBM_Stocks_Accounts_Domain.Response;
using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Interfaces;

namespace GBM_Stocks_Accounts_Infrastructure.Interfaces
{
    public interface IAccountRepository
    {
        ITransact Transact { get; set; }

        public Task<StoredSingleResponse<GetAccountResponse>> GetAccountById(GetAccountRequest getAccountRequest, string successMessage);

        public Task<StoredSingleResponse<CreateAccountResponse>> CreateAccount(CreateAccountRequest createAccountRequest, string successMessage);

        public Task<StoredSingleResponse<UpdateAccountResponse>> UpdateAccount(UpdateAccountRequest updateAccountRequest, string successMessage);
    }
}
