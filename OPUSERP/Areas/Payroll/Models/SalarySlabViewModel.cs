using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class SalarySlabViewModel
    {
        public int editId { get; set; }       
        public string slabName { get; set; }
        public int salaryGradeId { get; set; }
        public decimal slabAmount { get; set; }
        public DateTime? effectDate { get; set; }

        public IEnumerable<SalarySlab> salarySlabsList { get; set; }
        public SalarySlab salarySlab { get; set; }
        public IEnumerable<SalaryGrade> salaryGradesList { get; set; }
    }
}
