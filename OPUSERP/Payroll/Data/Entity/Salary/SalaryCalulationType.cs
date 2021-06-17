using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SalaryCalulationType", Schema = "Payroll")]
    public class SalaryCalulationType : Base
    {
        [MaxLength(100)]
        public string salaryCalulationTypeName { get; set; }    
    }
}
