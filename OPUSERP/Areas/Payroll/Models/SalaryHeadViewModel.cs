using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class SalaryHeadViewModel
    {
        public int editId { get; set; }
        public string salaryHeadName { get; set; }
        public string salaryHeadCode { get; set; }
        public string salaryHeadType { get; set; }
        public int? sortOrder { get; set; }
        public string isIncomeTax { get; set; }
        public string isInvestments { get; set; }
        public string isAdvance { get; set; }
        public string isArrear { get; set; }
        public string isBonus { get; set; }
        public string isMonthlyAllowance { get; set; }
        public string headShortName { get; set; }
        public string isLoan { get; set; }

        public IEnumerable<SalaryHead> salaryHeadsList { get; set; }
        public SalaryHead salaryHead { get; set; }
    }
}
