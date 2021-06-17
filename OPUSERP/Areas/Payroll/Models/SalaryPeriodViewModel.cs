using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class SalaryPeriodViewModel
    {
        public int editId { get; set; }
        public int salaryYearId { get; set; }
        public int taxYearId { get; set; }
        public int salaryTypeId { get; set; }
        public int? bonusTypeId { get; set; }
        public int? organizationsBranchId { get; set; }
        public string periodName { get; set; }
        public string monthName { get; set; }
        public int? lockLabel { get; set; }
        public decimal? daysWorking { get; set; }
        public string mailText { get; set; }
        public string mailSub { get; set; }

        public IEnumerable<SalaryPeriod> salaryPeriodsList { get; set; }
        public SalaryPeriod salaryPeriod { get; set; }
        public IEnumerable<SalaryYear> salaryYearsList { get; set; }
        public IEnumerable<SalaryType> salaryTypesList { get; set; }
        public IEnumerable<TaxYear> taxYearsList { get; set; }
        public IEnumerable<BonusType> bonusTypesList { get; set; }
    }
}
