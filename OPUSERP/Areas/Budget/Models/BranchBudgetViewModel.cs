using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Budget.Models
{
    public class BranchBudgetViewModel
    {
        public int? reqId { get; set; }

        public string reqNo { get; set; }

        public DateTime? reqDate { get; set; }

        public string fiscalYear { get; set; }

        public decimal? amount { get; set; }

        public string reqBy { get; set; }

        public string finalApproval { get; set; }

    }
}
