using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class InvestmentRebateSettingViewModel
    {
        public int investmentRebateSettingId { get; set; }


        public int taxYearId { get; set; }



        public decimal allowableInvestment { get; set; }
        
        public decimal investmentRebate { get; set; }
        public decimal orInvestmentRebate { get; set; }



        public IEnumerable<TaxYear> taxYears { get; set; }
        public TaxYear taxYear { get; set; }
       
        public IEnumerable<InvestmentRebateSettings> InvestmentRebateSettings { get; set; }
        public InvestmentRebateSettings investmentRebateSetting { get; set; }
       
    }
}
