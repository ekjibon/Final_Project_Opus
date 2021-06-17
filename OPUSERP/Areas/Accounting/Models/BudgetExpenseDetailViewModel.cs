using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class BudgetExpenseDetailViewModel
    {
        public string MainHead { get; set; }
        public string SubHead { get; set; }
        public string Head { get; set; }
        public decimal? BudgetAmount { get; set; }
        public decimal? CurrentAmount { get; set; }
        public decimal? PreviuosAmount { get; set; }
        public decimal? TotalExpense { get; set; }
        public decimal? BurnRate { get; set; }
        public decimal? BudgetBalance { get; set; }

    }
}
