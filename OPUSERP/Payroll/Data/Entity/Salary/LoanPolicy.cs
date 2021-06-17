using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("LoanPolicy", Schema = "Payroll")]
    public class LoanPolicy : Base
    {
        public int? salaryGradeId { get; set; }
        public SalaryGrade salaryGrade { get; set; }

        public int? salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }

        [MaxLength(300)]
        public string loanPolicyName { get; set; }
        public decimal? maximumLoanAmount { get; set; }
        public decimal? loanInterestRate { get; set; }
        public int? loanNoOfInstallment { get; set; }
        [MaxLength(10)]
        public string isActive { get; set; }
    }
}
