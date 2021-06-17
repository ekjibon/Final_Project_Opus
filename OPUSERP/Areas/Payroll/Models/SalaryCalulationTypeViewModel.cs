using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class SalaryCalulationTypeViewModel
    {
        public int editId { get; set; }
        public string salaryCalulationTypeName { get; set; }        

        public IEnumerable<SalaryCalulationType> salaryCalulationTypesList { get; set; }
        public SalaryCalulationType salaryCalulationType { get; set; }
    }
}
