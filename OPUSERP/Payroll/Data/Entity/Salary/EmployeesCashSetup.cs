using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("EmployeesCashSetup", Schema = "Payroll")]
    public class EmployeesCashSetup : Base
    {
        public int employeeInfoId { get; set; }
        public EmployeeInfo employeeInfo { get; set; }       

        public decimal? bankAmount { get; set; }
        public decimal? walletAmount { get; set; }
        public decimal? cashAmount { get; set; }
        [MaxLength(10)]
        public string defaultAccount { get; set; }
    }
}
