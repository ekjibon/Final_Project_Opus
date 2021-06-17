using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Budget.Data.Entity
{
    [Table("BudgetHeadDetail", Schema = "Budget")]
    public class BudgetHeadDetail: Base
    {
        public int? ledgerId { get; set; }
        public Ledger ledger { get; set; }

        public int? budgetHeadId { get; set; }
        public BudgetHead budgetHead { get; set; }
       
    }
}
