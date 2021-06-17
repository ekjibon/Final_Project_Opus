using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("WagesProcessEmpSalaryStructure", Schema = "Payroll")]
    public class WagesProcessEmpSalaryStructure : Base
    {
        public int employeeInfoId { get; set; }
        public WagesEmployeeInfo employeeInfo { get; set; }

        public int salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }

        public int salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }

        public decimal amount { get; set; }

    }
}
