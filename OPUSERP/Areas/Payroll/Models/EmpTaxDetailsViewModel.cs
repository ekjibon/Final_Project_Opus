using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class EmpTaxDetailsViewModel
    {
  
        public string empName { get; set; }
        public string employeeCode { get; set; }
        public string gender { get; set; }
        public string salaryHeadName { get; set; }
        public string assessmentYear { get; set; }
        public decimal? amount { get; set; }
        public decimal? exempAmount { get; set; }
        public decimal? taxableAmount { get; set; }
        public string periodName { get; set; }
        public string salaryHeadType { get; set; }
        public string taxYearName { get; set; }
        public string exempRule { get; set; }
        public decimal? loanAmount { get; set; }
        public string loanRule { get; set; }
        public int? thisYearAdjustment { get; set; }
        public string dateOfJoin { get; set; }
        public string tinNo { get; set; }
       
    }
}
