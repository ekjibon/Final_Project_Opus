using System.Collections.Generic;

namespace OPUSERP.Areas.SCMPurchaseOrder.Models
{
    public class PurchaseOrderDismissViewModel
    {
        public int? purchaseId { get; set; }
        public string PONo { get; set; }
        public string ReqNo { get; set; }
        public string poDate { get; set; }
        public string deliveryDate { get; set; }
        public string billToLocation { get; set; }
        public string personName { get; set; }
        public string organizationName { get; set; }
        public int? projectId { get; set; }
        public string projectName { get; set; }
        public string poValue { get; set; }

        public IEnumerable<PurchaseOrderDismissViewModel> purchaseOrderDismisses { get; set; }
    }
}
