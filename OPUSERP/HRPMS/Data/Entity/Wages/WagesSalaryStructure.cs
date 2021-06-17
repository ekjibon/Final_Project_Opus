using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Wages
{
    [Table("WagesSalaryStructure", Schema = "HR")]
    public class WagesSalaryStructure : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public WagesEmployeeInfo employee { get; set; }

        public int? salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }

        public decimal? amount { get; set; }
    }
}
