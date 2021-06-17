using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Models
{
    public class TaxableamountViewModel
    {
        public string accountName{ get; set; }
        public string exemtedrule { get; set; }
        public decimal? amount { get; set; }
        public decimal? exemAmount { get; set; }
        public decimal? taxableAmount { get; set; }
        public decimal? monthNo  { get; set; }
       // public IEnumerable<TaxableSlabViewModel> taxableSlabViewModels { get; set; }
    }
}
