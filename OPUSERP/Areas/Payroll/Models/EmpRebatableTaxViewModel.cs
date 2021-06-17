using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class EmpRebatableTaxViewModel
    {
        public decimal? taxLimitAmount { get; set; }
        public decimal? allowableInvestment { get; set; }
        public decimal? investmentRebate { get; set; }
        public decimal? oRInvestmentRebate { get; set; }
        public decimal? taxableAmount { get; set; }
        public decimal? pFAmount { get; set; }
        public decimal? rebatAmount { get; set; }
        public decimal? taxableIncome { get; set; }
        public decimal? rebatTax { get; set; }
        public decimal? totalTax { get; set; }
        public decimal? netTax { get; set; }
    }
}
