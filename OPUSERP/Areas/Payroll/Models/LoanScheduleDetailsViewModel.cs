using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class LoanScheduleDetailsViewModel
    {
        public decimal?[] selectPaymentHeadAmounts { get; set; }
        public int?[] selectPaymentHeadIds { get; set; }

        public int Id { get; set; }

        public IEnumerable<LoanScheduleMaster> loanScheduleMasters { get; set; }
        public IEnumerable<LoanScheduleDetail> loanScheduleDetails { get; set; }
        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }
        public IEnumerable<SalaryHead> salaryHeads { get; set; }
        public IEnumerable<LoanPolicy> loanPolicies { get; set; }
    }
}
