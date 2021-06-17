using OPUSERP.Accounting.Data.Entity.NonPoTransaction;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class VatTaxRateViewModel
    {
        public int vatTaxRateId { get; set; }
        public int rateTypeId { get; set; }

        public int taxYearId { get; set; }

        [Required]
        public decimal? rate { get; set; }
        public IEnumerable<RateType> rateTypes { get; set; }
        public IEnumerable<TaxYear> taxYears { get; set; }
        public IEnumerable<VatTaxRate> vatTaxRates { get; set; }
    }
}
