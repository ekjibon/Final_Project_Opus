using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("CodeManagement", Schema = "Payroll")]
    public class CodeManagement : Base
    {        
       
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public int? salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }

        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
        public string elementCode { get; set; }
        public string  percentAmount { get; set; }       

       
    }
}
