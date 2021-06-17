using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMStock.Models
{
    public class StockBalanceViewModel
    {
        public int ID { get; set; }
        public int? ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ItemCode { get; set; }
        public string MainCatName { get; set; }
        public string SubCatName { get; set; }
        public string ItemName { get; set; }
        public decimal? ItemReceive { get; set; }
        public decimal? ItemIssue { get; set; }
        public decimal? ItemTransfer { get; set; }
        public decimal? Balance { get; set; }
        public string BalanceCol { get; set; }
        public decimal? ItemTransferReceive { get; set; }
    }
}
