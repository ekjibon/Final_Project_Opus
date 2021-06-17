using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class EmployeeArrearViewModel
    {
        public int employeeArrearId { get; set; }
        public int employeeInfoId { get; set; }
        public int salaryPeriodId { get; set; }
        public int salaryHeadId { get; set; }
        public decimal? arrearAmount { get; set; }
        public decimal? ratio { get; set; }
        public decimal? txtBonus { get; set; }

        public IEnumerable<EmployeeArrear> employeeArrears { get; set; }
        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }

        public string visualEmpCodeName { get; set; }
    }
}
