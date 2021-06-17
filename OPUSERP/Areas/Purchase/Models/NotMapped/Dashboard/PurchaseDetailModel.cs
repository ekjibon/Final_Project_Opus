using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Purchase.Models.NotMapped.Dashboard
{
    public class PurchaseDetailModel
    {
        public string invoiceNumber { get; set; }
        public string invoiceDate { get; set; }
        public string  supplierName { get; set; }
        public decimal? netAmount { get; set; }
        
    }
}
