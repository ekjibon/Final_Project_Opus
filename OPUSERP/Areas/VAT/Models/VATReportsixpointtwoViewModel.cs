using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VAT.Models
{
    public class VATReportsixpointtwoViewModel
    {

        public int? specid { get; set; }
        public DateTime? Date { get; set; }
        public decimal? preproductionQuantity { get; set; }
        public decimal? preproductionPrice { get; set; }
        public string billNo { get; set; }
        public DateTime? BillDate { get; set; }
        public string saler { get; set; }
        public string salerAddress { get; set; }
        public string salerId { get; set; }
        public string description { get; set; }
        public decimal? amount { get; set; }
        public decimal? priceWithoutVAT { get; set; }
        public decimal? priceWithVAT { get; set; }
        public decimal? VAT { get; set; }
        public decimal? productionAmount { get; set; }
        public decimal? productionPrice { get; set; }
        public decimal? currentAmount { get; set; }
        public decimal? currentPrice { get; set; }
        public string comments { get; set; }
      //  public IEnumerable<VATReportsixpointtwoSumViewModel> vATReportsixpointoneSumViewModels { get; set; }




    }
}
