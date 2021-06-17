using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("EmployeeArrear", Schema = "Payroll")]
    public class EmployeeArrear : Base
    {
        public int employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }        

        public int salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }

        public int salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }

        public decimal arrearAmount { get; set; }
        public decimal ratio { get; set; }
       
        [NotMapped]
        public string salaryHeadName { get; set; }
    }
}
