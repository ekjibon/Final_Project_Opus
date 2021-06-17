using OPUSERP.Data.Entity;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Data.Entity.NonPoTransaction
{
    [Table("VatTaxRate", Schema = "ACCOUNT")]
    public class VatTaxRate:Base
    {
        public int? rateTypeId { get; set; }
        public RateType rateType { get; set; }

        public int? taxYearId { get; set; }
        public TaxYear taxYear { get; set; }

        public decimal? rate { get; set; }

    }
}
