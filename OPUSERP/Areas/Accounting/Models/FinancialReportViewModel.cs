using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class FinancialReportViewModel
    {
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<ProfitAndLossAccountViewModel> profitAndLossAccountViewModels { get; set; }
        public IEnumerable<BalanceSheetViewModel>  balanceSheetViewModels { get; set; }
        public IEnumerable<CFSMasterViewModel> cFSMasterViewModels { get; set; }
        public IEnumerable<BudgetExpenseMasterViewModel> budgetExpenseMasterViewModels { get; set; }
        public IEnumerable<BudgetExpenseMasterPViewModel> budgetExpenseMasterPViewModels { get; set; }
        public IEnumerable<RVMasterViewModel> rVMasterViewModels { get; set; }
        public Project project { get; set; }
    }
}
