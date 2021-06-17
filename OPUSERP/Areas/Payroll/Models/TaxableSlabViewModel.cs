using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class TaxableSlabViewModel
    {
        public string calculationOfInvestment{ get; set; }
        public decimal rate { get; set; }
        public decimal? slabamount { get; set; }
        public decimal? taxAmount { get; set; }
    }
}
