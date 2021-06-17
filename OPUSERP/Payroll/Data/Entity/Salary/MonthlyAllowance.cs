using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("MonthlyAllowance", Schema = "Payroll")]
    public class MonthlyAllowance : Base
    {
        public int? employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }       

        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }

        public int? salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }

        public decimal? allowanceAmount { get; set; }
        
        [MaxLength(400)]
        public string remarks { get; set; }
    }
}
