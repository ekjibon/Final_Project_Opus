using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Budget.Data.Entity
{
    [Table("BudgetDisbursementDetail", Schema = "Budget")]
    public class BudgetDisbursementDetail: Base
    {
        public int? budgetDisbursementMasterId { get; set; }
        public BudgetDisbursementMaster budgetDisbursementMaster { get; set; }

        public int? budgetHeadId { get; set; }
        public BudgetHead budgetHead { get; set; }

       
        [Column(TypeName = "decimal(18,2)")]
        public decimal? amount { get; set; }
        
    }
}
