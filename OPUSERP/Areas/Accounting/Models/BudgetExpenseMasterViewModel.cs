using OPUSERP.Budget.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class BudgetExpenseMasterViewModel
    {

        public string fromDate { get; set; }
        public string toDate { get; set; }

        public IEnumerable<BudgetExpenseDetailViewModel> budgetExpenseDetailViewModels{ get; set; }
       
        public Company Company { get; set; }
        public IEnumerable<FiscalYear> fiscalYears { get; set; }
        
    }
}
