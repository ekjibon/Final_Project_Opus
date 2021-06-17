using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.PurchaseOrder
{
    [Table("PODismissDetails", Schema = "SCM")]
    public class PODismissDetail:Base
    {
        public int? pODismissId { get; set; }
        public PODismissMaster pODismiss { get; set; }

        public int? purchaseDetailsId { get; set; }
        public PurchaseOrderDetails purchaseDetails { get; set; }

        public decimal? poQty { get; set; }
        public decimal? dismissQty { get; set; }
    }
}
