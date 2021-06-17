using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMStock.Models
{
    public class StockInViewModel
    {
        public string MRNO { get; set; }
        public string remarks { get; set; }
        public int?[] detailids { get; set; }
        public int?[] itemPriceId { get; set; }
        public decimal?[] rates { get; set; }
        public int?[] specids { get; set; }
        public decimal?[] quantitys { get; set; }
        public decimal?[] duequantitys { get; set; }
        public DateTime stockDate { get; set; }
        public DateTime? paymentDate { get; set; }
        public int? stockMasterId { get; set; }
        public int? purchaseId { get; set; }
        public int? iouId { get; set; }
        public int? resouceId { get; set; }

        public IEnumerable<StockItemViewModel> stockItemViewModels { get; set; }
        public PurchaseOrderMaster PurchaseOrderMaster { get; set; }
        public IOUMaster iOUMaster { get; set; }
    }
}
