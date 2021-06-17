using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class IncomeTaxSetupViewModel
    {
        public int IncomeTaxSettupId { get; set; }
        public int salaryHeadId { get; set; }
   

        public int taxYearId { get; set; }


        public string exemption { get; set; }
        public decimal exemptionAmount { get; set; }
        public decimal exemptionPercent { get; set; }
        public decimal exemptionMaxAmount { get; set; }
        public int? sortOrder { get; set; }

        public string exemptionRule { get; set; }
        public IEnumerable<TaxYear> taxYearsList { get; set; }
        public TaxYear taxYear { get; set; }
        public IEnumerable<SalaryHead> salaryHeads { get; set; }
        public SalaryHead salaryHead { get; set; }
        public IEnumerable<IncomeTaxSetup> incomeTaxSetups { get; set; }
        public IncomeTaxSetup incomeTaxSetup { get; set; }
       
    }
}
