using System.ComponentModel.DataAnnotations;

namespace GBM_Stocks_Accounts_Domain.Response
{
    public partial class CreateAccountResponse
    {
        [Display(Name = "AccountId")]
        public int AccountId { get; set; }
    }
}
