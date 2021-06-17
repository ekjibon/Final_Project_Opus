using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.VAT.Models
{
    public class VATReportsixpointtwopointoneViewModel
    {

        public int? specid { get; set; }
        public DateTime? Date { get; set; }
        public decimal? preQuantity { get; set; }
        public decimal? prePrice { get; set; }
        public decimal? purQuantity { get; set; }
        public decimal? purPrice { get; set; }
        public decimal? totalQuantity { get; set; }
        public decimal? totalPrice { get; set; }
        public string billNo { get; set; }
        public DateTime? BillDate { get; set; }
        public string saler { get; set; }
        public string salerAddress { get; set; }
        public string salerId { get; set; }
        public string itemName { get; set; }
        public decimal? amount { get; set; }
        public decimal? priceWithoutVAT { get; set; }

        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public string customerId { get; set; }
        public string invoiceNo { get; set; }
        public DateTime? invoicedate { get; set; }

        public decimal? musok { get; set; }
        public decimal? saleAmount { get; set; }
        public decimal? saleprice { get; set; }


        public decimal? VAT { get; set; }
   
        public decimal? currentAmount { get; set; }
        public decimal? currentPrice { get; set; }
        public string comments { get; set; }
        public int? itemspecid { get; set; }
       // public IEnumerable<VATReportsixpointoneSumViewModel> vATReportsixpointoneSumViewModels { get; set; }




    }
}
