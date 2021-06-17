using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SlabRebate", Schema = "Payroll")]
    public class SlabRebate : Base
    {
        public int slabTypeId { get; set; }
        public RebateSlabType slabType { get; set; }

        public int taxYearId { get; set; }
        public TaxYear taxYear { get; set; }             

        public decimal slabRebateAmount { get; set; }
        public decimal taxRate { get; set; }       
        public int? sortOrder { get; set; }
        [MaxLength(250)]
        public string slabRebateText { get; set; }
    }
}
