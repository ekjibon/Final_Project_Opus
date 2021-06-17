using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("EmployeesTax", Schema = "Payroll")]
    public class EmployeesTax:Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public int? taxYearId { get; set; }
        public TaxYear taxYear { get; set; }
        public decimal? yearlyTaxableincome { get; set; }
        public decimal? yearlyTaxableamount { get; set; }
        public decimal? amount { get; set; }
        public int? isActive { get; set; } //0 inactive 1 Active
    }
}
