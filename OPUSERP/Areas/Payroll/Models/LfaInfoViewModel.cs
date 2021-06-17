using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class LfaInfoViewModel
    {
        public int lfaInfoId { get; set; }
        public int employeeInfoId { get; set; }
        public int salaryPeriodId { get; set; }

        public decimal lfaAmount { get; set; }
        public DateTime? lfaStartDate { get; set; }
        public DateTime? lfaEndDate { get; set; }
        public int currentLfaYearNo { get; set; }        
        public string lfaStatus { get; set; }

        public IEnumerable<LfaInfo> lfaInfos { get; set; }
        public IEnumerable<SalaryPeriod> salaryPeriods { get; set; }

        public string visualEmpCodeName { get; set; }
    }
}
