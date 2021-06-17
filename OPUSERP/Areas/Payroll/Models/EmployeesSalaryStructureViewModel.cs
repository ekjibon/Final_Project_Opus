using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class EmployeesSalaryStructureViewModel
    {
        public int editId { get; set; }       
        public int employeeInfoId { get; set; }
        public int? salaryGradeId { get; set; }
        public int? salarySlab { get; set; }
        //public int salaryYearId { get; set; }
        public decimal amount { get; set; }
        public string isActive { get; set; }
        public DateTime? effectiveDate { get; set; }

        public IEnumerable<EmployeesSalaryStructure> employeesSalaryStructuresList { get; set; }
        public EmployeesSalaryStructure employeesSalaryStructure { get; set; }
        public IEnumerable<SalaryGrade> salaryGradesList { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
    }
}
