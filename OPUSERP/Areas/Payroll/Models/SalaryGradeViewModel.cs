using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class SalaryGradeViewModel
    {
        public int salaryGradeId { get; set; }
        public string gradeName { get; set; }
        public decimal? basicAmount { get; set; }
        public string payScale { get; set; }
        public decimal? currentBasic { get; set; }

        public IEnumerable<SalaryGrade> salaryGrades { get; set; }
    }
}
