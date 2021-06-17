using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class EditsalarystructureViewModel
    {
        public int editsalarystructureId { get; set; }  
        public decimal headamount { get; set; }
        public string salarystructureStatus { get; set; }  
    }
}
