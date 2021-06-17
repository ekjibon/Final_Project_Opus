using OPUSERP.Payroll.Data.Entity.Salary;
using System.Collections.Generic;

namespace OPUSERP.Areas.Payroll.Models
{
    public class SalaryStatusLogViewModel
    {        
        public int? salaryPeriodLoadId { get; set; }
        public string statusType { get; set; } 
        public string remarks { get; set; }
        public int? draftFinalId { get; set; }

        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }
        
    }
}
