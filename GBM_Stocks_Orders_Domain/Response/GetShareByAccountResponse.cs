using System.ComponentModel.DataAnnotations;

namespace GBM_Stocks_Orders_Domain.Response
{
    public partial class GetShareByAccountResponse
    {
        [Display(Name = "IssuerName")]
        public int IssuerName { get; set; }
        [Display(Name = "SharePrice")]
        public decimal SharePrice { get; set; }
        [Display(Name = "TotalShare")]
        public int TotalShare { get; set; }
    }
}
