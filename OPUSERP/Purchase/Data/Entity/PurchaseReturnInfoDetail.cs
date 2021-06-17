using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Purchase.Data.Entity
{
    [Table("PurchaseReturnInfoDetail", Schema = "Purchase")]
    public class PurchaseReturnInfoDetail:Base
    {
        public int? purchaseReturnInfoMasterId { get; set; }
        public PurchaseReturnInfoMaster purchaseReturnInfoMaster { get; set; }

        public int? purchaseOrdersDetailId { get; set; }
        public PurchaseOrderDetails purchaseOrdersDetail { get; set; }

        public decimal? quantity { get; set; }
    }
}
