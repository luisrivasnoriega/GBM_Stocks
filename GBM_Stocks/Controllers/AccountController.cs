using GBM_Stocks_Accounts_Core.Interfaces;
using GBM_Stocks_Accounts_Core.Services;
using GBM_Stocks_Accounts_Domain.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GBM_Stocks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService AccountService;

        public AccountController(IAccountService accountService)
        {
            AccountService = accountService;
        }

        [HttpGet("Get")]
        public async Task<ActionResult> GetAccount([FromQuery] GetAccountRequest getAccountRequest)
        {
            var response = await AccountService.GetAccountById(getAccountRequest, "Get account");
            return Ok(response);
        }

        [HttpPost("Post")]
        public async Task<ActionResult> PostAccount(CreateAccountRequest createAccountRequest)
        {
            var response = await AccountService.CreateAccount(createAccountRequest, "Create account");
            return Ok(response);
        }
    }
}