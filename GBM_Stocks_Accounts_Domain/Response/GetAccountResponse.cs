using System.ComponentModel.DataAnnotations;

namespace GBM_Stocks_Accounts_Domain.Response
{
    public partial class GetAccountResponse
    {
        public GetAccountResponse() {
            Issuers = new List<string>();
        } 

        [Display(Name = "AccountId")]
        public int AccountId { get; set; }
        [Display(Name = "Cash")]
        public decimal Cash { get; set; }
        public List<string> Issuers { get; set; }
    }
}
