using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SalaryStatusLog", Schema = "Payroll")]
    public class SalaryStatusLog : Base
    {        
        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
        [MaxLength(200)]
        public string statusType { get; set; }
        [MaxLength(40)]
        public string ipAddress { get; set; }
        public string remarks { get; set; }


    }
}
