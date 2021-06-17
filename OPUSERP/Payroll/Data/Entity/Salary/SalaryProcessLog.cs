using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SalaryProcessLog", Schema = "Payroll")]
    public class SalaryProcessLog : Base
    {        
        public int? salaryPeriodId { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
        [MaxLength(30)]
        public string processType { get; set; }
        [MaxLength(30)]
        public string ipAddress { get; set; }
       


    }
}
