using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("LoanRoute", Schema = "Payroll")]
    public class LoanRoute : Base
    {
        public int? loanScheduleMasterId { get; set; }
        public LoanScheduleMaster loanScheduleMaster { get; set; }

        //Supervisor Id
        public int? employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? routeOrder { get; set; }
        public int? isActive { get; set; }
    }
}
