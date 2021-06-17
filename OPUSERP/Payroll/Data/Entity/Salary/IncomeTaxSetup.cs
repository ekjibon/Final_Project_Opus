using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("IncomeTaxSetup", Schema = "Payroll")]
    public class IncomeTaxSetup : Base
    {
        public int salaryHeadId { get; set; }
        public SalaryHead salaryHead { get; set; }

        public int taxYearId { get; set; }
        public TaxYear taxYear { get; set; }

        public string exemption { get; set; }
        public decimal exemptionAmount { get; set; }
        public decimal exemptionPercent { get; set; }
        public decimal exemptionMaxAmount { get; set; }
        [MaxLength(400)]
        public string exemptionRule { get; set; }
        public int? sortOrder { get; set; }
        
    }
}
