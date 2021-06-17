using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("LoanScheduleMaster", Schema = "Payroll")]
    public class LoanScheduleMaster:Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int? loanPolicyId { get; set; }
        public LoanPolicy loanPolicy { get; set; }

        public DateTime? loanDate { get; set; }
        public decimal? totalLoanAmount { get; set; }
        public decimal? installmentAmount { get; set; }
        public int? noOfInstallment { get; set; }

        public decimal? interestRate { get; set; }
        public decimal? interestAmount { get; set; }
        public decimal? TotalLoanWithInterestAmount { get; set; }

        [DefaultValue(0)]
        public int? isContinue { get; set; }
        [DefaultValue(0)]
        public int? isComplete { get; set; }
        [MaxLength(600)]
        public string purpose { get; set; }
        [DefaultValue(0)]
        public int? approveStatus { get; set; } //0=Apply,1=Approve
        [MaxLength(300)]
        public string fileUrl { get; set; }
    }
}
