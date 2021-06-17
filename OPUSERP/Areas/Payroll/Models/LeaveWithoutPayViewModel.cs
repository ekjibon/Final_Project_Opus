using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class LeaveWithoutPayViewModel
    {
        public int leaveWithoutPayId { get; set; }
        public int employeeInfoId { get; set; }
        public int salaryPeriodId { get; set; }
        public int noOfDays { get; set; }

        public IEnumerable<LeaveWithoutPay> leaveWithoutPays { get; set; }
        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }

        public string visualEmpCodeName { get; set; }
    }
}
