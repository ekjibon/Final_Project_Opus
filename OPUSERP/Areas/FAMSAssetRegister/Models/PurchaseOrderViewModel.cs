using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSAssetRegister.Models
{
    public class PurchaseOrderViewModel
    {
        public int? purchaseId { get; set; }
        public string PONO{ get; set; }
        public int? supplierId { get; set; }
        public string supplierName { get; set; }
        public DateTime? purchaseDate { get; set; }
    
    }
}
