using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("TaxYear", Schema = "Payroll")]
    public class TaxYear : Base
    {
        [MaxLength(100)]
        public string taxYearName { get; set; }
        
        public DateTime? startDate { get; set; }
        
        public DateTime? endDate { get; set; }

        public decimal? taxLimit { get; set; }

        public int? lockLabel { get; set; }
        public decimal? allowablePerquisite { get; set; }
        [MaxLength(100)]
        public string assessmentYear { get; set; }
    }
}
