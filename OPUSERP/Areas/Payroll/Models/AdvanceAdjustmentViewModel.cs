using OPUSERP.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class AdvanceAdjustmentViewModel
    {
        public int advanceAdjustmentId { get; set; }
        public int employeeInfoId { get; set; }
        public int salaryPeriodId { get; set; }
        public int salaryHeadId { get; set; }

        public DateTime? date { get; set; }

        public decimal advanceAmount { get; set; }
        public decimal installmentAmount { get; set; }
        public int noOfInstallment { get; set; }       
        public int? isContinue { get; set; }       
        public int? isComplete { get; set; }        
        public string purpose { get; set; }

        public IEnumerable<AdvanceAdjustment> advanceAdjustments { get; set; }
        public IEnumerable<AdvanceAdjustmentDetail> advanceAdjustmentDetails { get; set; }
        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }
        public IEnumerable<SalaryHead> salaryHeads { get; set; }
        public IEnumerable<Company> companies { get; set; }

        public string visualEmpCodeName { get; set; }
    }
}
