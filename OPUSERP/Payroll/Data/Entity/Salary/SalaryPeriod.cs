using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SalaryPeriod", Schema = "Payroll")]
    public class SalaryPeriod : Base
    {
        public int salaryYearId { get; set; }
        public SalaryYear salaryYear { get; set; }

        public int taxYearId { get; set; }
        public TaxYear taxYear  { get; set; }

        public int salaryTypeId { get; set; }
        public SalaryType salaryType { get; set; }

        public int? bonusTypeId { get; set; }
        public BonusType bonusType { get; set; }

        public int? organizationsBranchId { get; set; }
        [MaxLength(100)]
        public string periodName { get; set; }
        [MaxLength(10)]
        public string monthName { get; set; }

        public int? lockLabel { get; set; }
        [MaxLength(100)]
        public string lockBy { get; set; }
        public DateTime? lockDate { get; set; }

        public decimal? daysWorking { get; set; }
        
        public string mailText { get; set; }
        [MaxLength(250)]
        public string mailSub { get; set; }

    }
}
