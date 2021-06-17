using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class SalaryGradePercentViewModel
    {
        public int editId { get; set; }       
        public int salaryHeadId { get; set; }
        public int salaryGradeId { get; set; }
        public int salaryCalulationTypeId { get; set; }
        public decimal percentAmount { get; set; }       

        public IEnumerable<SalaryGradePercent> salaryGradePercentsList { get; set; }
        public SalarySlab salarySlab { get; set; }
        public IEnumerable<SalaryGrade> salaryGradesList { get; set; }
        public IEnumerable<SalaryHead> salaryHeadsList { get; set; }
        public IEnumerable<SalaryCalulationType> salaryCalulationTypesList { get; set; }
    }
}
