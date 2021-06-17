using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class LoanScheduleViewModel
    {
        public int advanceAdjustmentId { get; set; }
        public int employeeInfoId { get; set; }
        public int salaryPeriodId { get; set; }
        public int salaryHeadId { get; set; }
        public int? loanPolicyId { get; set; }

        public decimal? advanceAmount { get; set; }
        public decimal? interestRate { get; set; }
        public decimal? interestAmount { get; set; }
        public decimal? totalAmountWithInterest { get; set; }

        public decimal? installmentAmount { get; set; }
        public int? noOfInstallment { get; set; }
        public int? isContinue { get; set; }
        public int? isComplete { get; set; }
        public string purpose { get; set; }

        public decimal?[] selectPaymentHeadAmounts { get; set; }
        public int?[] selectPaymentHeadIds { get; set; }

        public int?[] registerids { get; set; }
        public int?[] ids { get; set; }

        public DateTime?  date { get; set; }

        public IEnumerable<LoanScheduleMaster> loanScheduleMasters { get; set; }
        public IEnumerable<LoanScheduleDetail> loanScheduleDetails { get; set; }
        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }
        public IEnumerable<SalaryHead> salaryHeads { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public ApprovalDetail approvalDetail { get; set; }
        public IEnumerable<LoanRoute> loanRoutes { get; set; }
        public IEnumerable<LoanPolicy> loanPolicies { get; set; }
        public IEnumerable<LoanScheduleReportViewModel> loanScheduleReportViewModels { get; set; }

        public string visualEmpCodeName { get; set; }
    }
}
