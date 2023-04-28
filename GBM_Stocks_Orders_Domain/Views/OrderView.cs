using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBM_Stocks_Orders_Domain.Views
{
    public class OrderView
    {
        public int OrderId { get; set; }
        public string IssuerName { get; set; }
        public int Shares { get; set; }
        public decimal SharePrice { get; set; }
        public int Timestamp { get; set; }
        public int AccountId { get; set; }
        public bool Operation { get; set; }
    }

}
