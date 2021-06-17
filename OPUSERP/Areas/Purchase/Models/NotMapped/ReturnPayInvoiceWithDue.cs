
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Purchase.Models.NotMapped
{
    public class ReturnPayInvoiceWithDue
    {
       // public SalesReturnInvoiceMaster salesReturnInvoiceMaster { get; set; }

        public decimal? due { get; set; }
        public int? storeId { get; set; }
    }
}
