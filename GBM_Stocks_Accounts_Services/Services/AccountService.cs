using GBM_Stocks_Accounts_Core.Interfaces;
using GBM_Stocks_Accounts_Domain.Request;
using GBM_Stocks_Accounts_Domain.Response;
using GBM_Stocks_Accounts_Infrastructure.Interfaces;
using GBM_Stocks_Database.Domain;
using GBM_Stocks_Infrastructure.Interfaces;

namespace GBM_Stocks_Accounts_Core.Services
{
    public class AccountService : IAccountService
    {
        IAccountRepository AccountRepository { get; set; }
        IUnitOfWork UnitOfWork { get; set; }

        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            AccountRepository = accountRepository;
        }

        public async Task<StoredSingleResponse<GetAccountResponse>> GetAccountById(GetAccountRequest getAccountRequest, string successMessage)
        {
            return await AccountRepository.GetAccountById(getAccountRequest, successMessage);
        }

        public async Task<StoredSingleResponse<GetAccountResponse>> CreateAccount(CreateAccountRequest createAccountRequest, string successMessage)
        {
            var createdAccountResponse = await AccountRepository.CreateAccount(createAccountRequest, successMessage);
            await UnitOfWork.CommitAsync();

            var getAccountRequest = new GetAccountRequest();
            getAccountRequest.AccountId = createdAccountResponse.Response.AccountId;
            var account = await AccountRepository.GetAccountById(getAccountRequest, "Get Account");

            return account;
        }
    }
}
