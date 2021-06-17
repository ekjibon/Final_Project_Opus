using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class FsSalaryStructureViewModel
    {
        public string salaryHeadType { get; set; }
        public string salaryHeadName { get; set; }
        public int? salaryHeadId { get; set; }
        public decimal? amount { get; set; }       
    }
}
