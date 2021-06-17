using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SalaryType", Schema = "Payroll")]
    public class SalaryType : Base
    {
        [MaxLength(100)]
        public string salaryTypeName { get; set; }    
    }
}
