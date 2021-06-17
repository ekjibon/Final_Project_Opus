using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SalaryYear", Schema = "Payroll")]
    public class SalaryYear : Base
    {
        [MaxLength(100)]
        public string yearName { get; set; }
        
        public DateTime? startDate { get; set; }
       
        public DateTime? endDate { get; set; }

        public string yearStatus { get; set; }

        public DateTime? lockDate { get; set; }
    }
}
