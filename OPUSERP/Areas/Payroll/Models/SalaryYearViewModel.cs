using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class SalaryYearViewModel
    {
        public int editId { get; set; }
        public string yearName { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }

        public IEnumerable<SalaryYear> salaryYearsList { get; set; }
        //public SalaryYear salaryYear { get; set; }
    }
}
