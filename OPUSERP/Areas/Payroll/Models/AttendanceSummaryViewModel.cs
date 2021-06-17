using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class AttendanceSummaryViewModel
    {
        public int empAttendanceSummaryId { get; set; }
        public int employeeInfoId { get; set; }
        public int salaryPeriodId { get; set; }

        public int? weeklyOff { get; set; }
        public int? holiday { get; set; }
        public int? leave { get; set; }
        public int? present { get; set; }
        public int? absent { get; set; }
        public int? late { get; set; }
        public string remarks { get; set; }
        public string visualEmpCodeName { get; set; }

        public IEnumerable<EmpAttendanceSummary> attendanceSummaries { get; set; }
        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }
    }
}
