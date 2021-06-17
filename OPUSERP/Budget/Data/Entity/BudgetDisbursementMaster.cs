using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Budget.Data.Entity
{
    [Table("BudgetDisbursementMaster", Schema = "Budget")]
    public class BudgetDisbursementMaster : Base
    {
        public int? budgetBranchId { get; set; }
        public SpecialBranchUnit budgetBranch { get; set; }

        public int? fiscalYearId { get; set; }
        public FiscalYear fiscalYear { get; set; }
        [MaxLength(150)]
        public string disburseNo { get; set; }

        public DateTime? disburseDate { get; set; }

        public int? disburseBy { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? grandTotal { get; set; }

        public int? status { get; set; }

        public int? type { get; set; }
    }
}
