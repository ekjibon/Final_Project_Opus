using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.Payroll.Models
{
    public class BonusProcessViewModel
    {
        public int? employeeInfoId { get; set; }
        public int salaryPeriodId { get; set; }
        public int salaryHeadId { get; set; }
        public DateTime? lastDayofPeriod { get; set; }
        public string userName { get; set; }
        public string bonusFor { get; set; }

        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }
        public IEnumerable<SalaryHead> salaryHeads { get; set; }
    }
}
