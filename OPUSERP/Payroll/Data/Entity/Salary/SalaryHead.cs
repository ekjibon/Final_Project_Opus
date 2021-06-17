using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SalaryHead", Schema = "Payroll")]
    public class SalaryHead : Base
    {
        [MaxLength(100)]
        public string salaryHeadName { get; set; }
        [MaxLength(100)]
        public string salaryHeadCode { get; set; }
        [MaxLength(100)]
        public string salaryHeadType { get; set; }        

        public int? sortOrder { get; set; }
        [MaxLength(100)]
        public string isIncomeTax { get; set; }
        [MaxLength(100)]
        public string isInvestments { get; set; }
        [MaxLength(3)]
        public string isAdvance { get; set; }
        [MaxLength(3)]
        public string isArrear { get; set; }
        [MaxLength(3)]
        public string isBonus { get; set; }
        [MaxLength(3)]
        public string isMonthlyAllowance { get; set; }
        [MaxLength(50)]
        public string headShortName { get; set; }
        [MaxLength(3)]
        public string isLoan { get; set; }
    }
}
