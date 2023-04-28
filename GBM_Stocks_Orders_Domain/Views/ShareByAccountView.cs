using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBM_Stocks_Orders_Domain.Views
{

    public class ShareByAccountView
    {
        public string IssuerName { get; set; }
        public decimal SharePrice { get; set; }
        public int TotalShare { get; set; }
    }
}
