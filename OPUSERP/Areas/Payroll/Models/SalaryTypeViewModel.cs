using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class SalaryTypeViewModel
    {
        public int editId { get; set; }
        public string salaryTypeName { get; set; }        

        public IEnumerable<SalaryType> salaryTypesList { get; set; }
        public SalaryType salaryType { get; set; }
    }
}
