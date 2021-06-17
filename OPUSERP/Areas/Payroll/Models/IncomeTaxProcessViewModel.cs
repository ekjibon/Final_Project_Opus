using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class IncomeTaxProcessViewModel
    {
  
        public int employeeInfoId { get; set; }

        public int salaryPeriodId { get; set; }



   
        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
    
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public TaxProcessViewModel taxProcessViewModel { get; set; }
        public IEnumerable<TaxYear> taxYearlist { get; set; }
        public IEnumerable<EmployeesTax> employeesTaxes { get; set; }


    }
}
