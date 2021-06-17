using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class EmployeesSalaryActivityViewModel
    {
        public int salaryActivityPercentId { get; set; }
        public int? employeeProjectActivityId { get; set; }
        public int? employeeInfoId { get; set; }
        public decimal Amount { get; set; }

        public IEnumerable<SalaryActivityPercent> GetSalaryActivityPercents { get; set; }
        public SalaryActivityPercent salaryActivityPercent { get; set; }
        public IEnumerable<EmployeeProjectActivity> employeeProjectActivities { get; set; }
      
    }
}
