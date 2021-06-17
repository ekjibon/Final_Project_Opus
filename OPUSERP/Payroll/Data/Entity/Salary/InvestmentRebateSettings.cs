using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("InvestmentRebateSettings", Schema = "Payroll")]
    public class InvestmentRebateSettings : Base
    {       
        public int taxYearId { get; set; }
        public TaxYear taxYear { get; set; }
        
        public decimal allowableInvestment { get; set; }
        public decimal investmentRebate { get; set; }
        public decimal orInvestmentRebate { get; set; }
        
    }
}
