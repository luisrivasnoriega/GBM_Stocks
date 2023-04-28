using GBM_Stocks_Accounts_Domain.Request;
using GBM_Stocks_Accounts_Domain.Response;
using GBM_Stocks_Accounts_Infrastructure.Interfaces;
using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Interfaces;

namespace GBM_Stocks_Accounts_Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public ITransact Transact { get; set; }

        public AccountRepository(ITransact transact)
        {
            Transact = transact;
        }

        public async Task<StoredSingleResponse<GetAccountResponse>> GetAccountById(GetAccountRequest getAccountRequest, string successMessage)
        {
            var getAccountById = await Transact.SingleExecution<GetAccountRequest, GetAccountResponse>(getAccountRequest, successMessage);
            return getAccountById;
        }

        public async Task<StoredSingleResponse<CreateAccountResponse>> CreateAccount(CreateAccountRequest createAccountRequest, string successMessage)
        {
            var accountById = await Transact.SingleExecution<CreateAccountRequest, CreateAccountResponse>(createAccountRequest, successMessage);
            return accountById;
        }
    }
}
