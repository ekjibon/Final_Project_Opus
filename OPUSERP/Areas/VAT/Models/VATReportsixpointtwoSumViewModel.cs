using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VAT.Models
{
    public class VATReportsixpointtwoSumViewModel
    {
        public decimal? preproductionamount { get; set; }
        public decimal? preproductionPrice { get; set; }
        public decimal? amount { get; set; }
        public decimal? priceWithoutVAT { get; set; }
  
        public decimal? productionAmount { get; set; }
        public decimal? productionPrice { get; set; }
        public decimal? currentAmount { get; set; }
        public decimal? currentPrice { get; set; }
        public string itemname { get; set; }




    }
}
