using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("WagesBankInfo", Schema = "HR")]
    public class WagesBankInfo : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public WagesEmployeeInfo employee { get; set; }

        public string bankName { get; set; }

        public string branchName { get; set; }

        public string accountNumber { get; set; }

        public string ibus { get; set; }

        public decimal? basicSalary { get; set; }
    }
}