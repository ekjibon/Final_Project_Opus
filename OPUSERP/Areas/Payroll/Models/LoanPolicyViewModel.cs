using OPUSERP.Payroll.Data.Entity.Salary;
using System.Collections.Generic;

namespace OPUSERP.Areas.Payroll.Models
{
    public class LoanPolicyViewModel
    {
        public int editId { get; set; }
        public int? salaryGradeId { get; set; }
        public int? salaryHeadId { get; set; }

        public string loanPolicyName { get; set; }
        public decimal? maximumLoanAmount { get; set; }
        public decimal? loanInterestRate { get; set; }
        public int? loanNoOfInstallment { get; set; }
        public string isActive { get; set; }

        public IEnumerable<LoanPolicy> loanPolicies { get; set; }
        public IEnumerable<SalaryGrade> salaryGradesList { get; set; }
        public IEnumerable<SalaryHead> salaryHeadsList { get; set; }
    }
}
