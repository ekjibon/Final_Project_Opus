using OPUSERP.Budget.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Budget.Models
{
    public class DisbursmentViewModel
    {
        public string number { get; set; }
        public DateTime? date { get; set; }
        public int? year { get; set; }
        public int? branch { get; set; }

        public int? Id { get; set; }

        public int?[] headIdAll { get; set; }
        public decimal?[] amountAll { get; set; }

        public IEnumerable<FiscalYear> fiscalYears { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }
        public IEnumerable<BudgetDisbursementMaster> budgetDisbursementMasters { get; set; }
    }
}
