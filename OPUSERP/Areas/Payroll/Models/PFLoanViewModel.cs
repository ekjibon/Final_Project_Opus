using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class PFLoanViewModel
    {
        public int PfloanId { get; set; }
        public int employeeInfoId { get; set; }
        public int salaryPeriodId { get; set; }
        public int salaryHeadId { get; set; }
        public string PfLoanNo { get; set; }
        
        public decimal advanceAmount { get; set; }
        public decimal installmentAmount { get; set; }
        public int noOfInstallment { get; set; }       
        public int? isFromSalary { get; set; }
        public DateTime? loanDate { get; set; }
        public string purpose { get; set; }

        public IEnumerable<PFLoan> pFLoans { get; set; }
        public IEnumerable<PFLoanSchedule> pFLoanSchedules { get; set; }
        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }
        public IEnumerable<SalaryHead> salaryHeads { get; set; }

        public string visualEmpCodeName { get; set; }
    }
}
