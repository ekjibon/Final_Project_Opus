using OPUSERP.SCM.Data.Entity.PurchaseOrder;

namespace OPUSERP.Areas.Purchase.Models.NotMapped
{
    public class PurchaseInvoiceWithDue
    {
        public PurchaseOrderMaster purchaseOrderMaster { get; set; }


        public decimal? due { get; set; }
    }
}
