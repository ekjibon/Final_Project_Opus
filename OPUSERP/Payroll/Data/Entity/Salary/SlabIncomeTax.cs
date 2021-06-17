using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SlabIncomeTax", Schema = "Payroll")]
    public class SlabIncomeTax : Base
    {
        public int slabTypeId { get; set; }
        public SlabType slabType { get; set; }

        public int taxYearId { get; set; }
        public TaxYear taxYear { get; set; }             

        public decimal slabAmount { get; set; }
        public decimal taxRate { get; set; }       
        public int? sortOrder { get; set; }
        [MaxLength(100)]
        public string slabText { get; set; }
    }
}
