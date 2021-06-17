using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SalaryGradePercent", Schema = "Payroll")]
    public class SalaryGradePercent : Base
    {
        public int salaryGradeId { get; set; }
        public SalaryGrade salaryGrade { get; set; }

        public int salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }

        public int salaryCalulationTypeId { get; set; }
        public SalaryCalulationType salaryCalulationType { get; set; }

        public decimal? percentAmount { get; set; }

    }
}
