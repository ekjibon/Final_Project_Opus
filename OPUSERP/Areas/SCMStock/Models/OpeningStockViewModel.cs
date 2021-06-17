using OPUSERP.SCM.Data.Entity.Stock;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMStock.Models
{
    public class OpeningStockViewModel
    {
        public int opeiningId { get; set; }
        public int stockDetailId { get; set; }
        public int? itemSpecificationId { get; set; }
        public decimal? openingQty { get; set; }
        public decimal? openingRate { get; set; }
        public decimal? openingValue { get; set; }
        public DateTime? balanceUpTo { get; set; }
        public int? stockStatusId { get; set; }

        public IEnumerable<OpeningStock> openingStocks { get; set; }
        public OpeningStock openingStock { get; set; }
        public IEnumerable<StockDetails> stockDetails { get; set; }
    }
}
