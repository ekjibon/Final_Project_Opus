using OPUSERP.HRPMS.Data.Entity.Wages;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class WagesSalaryStructureViewModel
    {
        public int employeeId { get; set; }

        public int editId { get; set; }

        public int? salaryHeadId { get; set; }

        public decimal? amount { get; set; }

        public IEnumerable<SalaryHead> salaryHeads { get; set; }
        public IEnumerable<WagesSalaryStructure> wagesSalaryStructures { get; set; }
    }
}
