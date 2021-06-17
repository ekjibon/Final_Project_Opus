
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Purchase.Models.NotMapped.Dashboard
{
    public class DailyPurchaseModel
    {
        public decimal? numberOfPurchase { get; set; }
        public decimal? numberOfItem { get; set; }
        public decimal? purchase { get; set; }
        public decimal? payment { get; set; }  

        //public IEnumerable<GraphModel> graphModelspurchase { get; set; }
        //public IEnumerable<GraphModel> graphModelspayment { get; set; }
        public IEnumerable<PurchaseDetailModel> purchaseDetailModels { get; set; }
    }
}
