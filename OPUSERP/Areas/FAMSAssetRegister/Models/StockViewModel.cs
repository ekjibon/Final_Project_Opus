using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSAssetRegister.Models
{
    public class StockViewModel
    {
        public int? ItemSpecId { get; set; }
        public decimal? quantity { get; set; }
        public decimal? rate { get; set; }
        public DateTime? stockDate { get; set; }
        public string itemName { get; set; }
        public string categoryName { get; set; }
        public string itemCatPre { get; set; }
        public int? parentId { get; set; }
        public string specificationName { get; set; }
        public int? supplierId { get; set; }
        public string supplierName { get; set; }
        public int? purchaseId { get; set; }
    }
}
