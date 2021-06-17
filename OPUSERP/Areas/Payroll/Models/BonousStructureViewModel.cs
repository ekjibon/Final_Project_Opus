using OPUSERP.Payroll.Data.Entity.Salary;
using System.Collections.Generic;

namespace OPUSERP.Areas.Payroll.Models
{
    public class BonousStructureViewModel
    {
        public int bonousStructureId { get; set; }

        public int? BonousSubCategoryId { get; set; }
        public int? salaryCalulationTypeId { get; set; }
        public string monthYearType { get; set; }
        public string bonousBasedOn { get; set; }
        public int? isActive { get; set; }
        public int? minMonthValue { get; set; }
        public int? maxMonthValue { get; set; }
        public decimal? percentAmount { get; set; }

        public int? hasEmployee { get; set; }
        public int?[] ids { get; set; }

        public IEnumerable<BonousCategory> bonousCategories { get; set; }
        public IEnumerable<SalaryCalulationType> salaryCalulationTypes { get; set; }
        public IEnumerable<BonousStructure> bonousStructures { get; set; }
    }
}
