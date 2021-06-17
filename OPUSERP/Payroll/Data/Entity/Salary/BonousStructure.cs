using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("BonousStructure", Schema = "Payroll")]
    public class BonousStructure : Base
    {
        public int? BonousSubCategoryId { get; set; }
        public BonousSubCategory bonousSubCategory { get; set; }

        public int? salaryCalulationTypeId { get; set; }
        public SalaryCalulationType salaryCalulationType { get; set; }
        [MaxLength(5)]
        public string monthYearType { get; set; }
        [MaxLength(5)]
        public string bonousBasedOn { get; set; }
        
        public int? isActive { get; set; }
        public int? hasEmployee { get; set; }

        public int? minMonthValue { get; set; }
        public int? maxMonthValue { get; set; }
        public decimal? percentAmount { get; set; }

        
    }
}
