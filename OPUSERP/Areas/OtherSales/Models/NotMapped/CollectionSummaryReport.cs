using OPUSERP.OtherSales.Data.Entity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.OtherSales.Models.NotMapped
{
    public class CollectionSummaryReport
    {
        public IEnumerable<SalesCollection> salesCollection { get; set; }
        public IEnumerable<SalesInvoiceMaster> salesInvoiceMaster { get; set; }
        public decimal? totalInvoice { get; set; }
        public decimal? totalCollection { get; set; }
        public decimal? haveTopay { get; set; }
    }
}
