using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class BudgetExpenseMasterPViewModel
    {
        public IEnumerable<BudgetExpenseDetailPViewModel> budgetExpenseDetailViewModels { get; set; }
        public Company Company { get; set; }
        //public IEnumerable<FiscalYear> fiscalYears { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}