using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SalaryActivityPercent", Schema = "Payroll")]
    public class SalaryActivityPercent : Base
    {        
        public int? employeeProjectActivityId { get; set; }
        public EmployeeProjectActivity employeeProjectActivity { get; set; }
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }
        public decimal Amount { get; set; }       

       
    }
}
