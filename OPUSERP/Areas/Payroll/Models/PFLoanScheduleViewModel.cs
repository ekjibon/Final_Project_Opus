using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class PFLoanScheduleViewModel
    {
     
        public string periodName { get; set; }
        public int salaryPeriodId { get; set; }
        public decimal advanceAmount { get; set; }
        public decimal installmentAmount { get; set; }
        public int noOfInstallment { get; set; }       
        public decimal restAmount { get; set; }       
        public string status { get; set; }
       
    }
}
