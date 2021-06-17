using OPUSERP.OtherSales.Data.Entity.Sales;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.OtherSales.Models
{
    public class SalesMenuOpeningBalanceViewModel
    {
        public int opeiningId { get; set; }
        public int? itemSpecificationId { get; set; }
        public DateTime? openingDate { get; set; }
        public decimal? openingQty { get; set; }

        public IEnumerable<SalesMenuOpeningBalance> salesMenuOpeningBalances { get; set; }
        public SalesMenuOpeningBalance salesMenuOpeningBalance { get; set; }
    }
}
