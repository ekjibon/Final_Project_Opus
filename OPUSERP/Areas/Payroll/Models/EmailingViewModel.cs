using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class EmailingViewModel
    {
        public int? employeeInfoId { get; set; }
        public int? All { get; set; }
        public int? salaryPeriodId { get; set; }        
        public DateTime? payDate { get; set; }

        public string mailSub { get; set; }
        public string mailText { get; set; }

        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }
    }
}
