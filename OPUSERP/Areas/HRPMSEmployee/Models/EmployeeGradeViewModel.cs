using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class EmployeeGradeViewModel
    {
        public int id { get; set; }
        public int employeeId { get; set; }
        public int? salaryGradeId { get; set; }
        public DateTime? effectiveDate { get; set; }

        public string employeeNameCode { get; set; }
        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<SalaryGrade> salaryGrades { get; set; }
        public IEnumerable<EmployeeGrade> employeeGrades { get; set; }
    }
}
