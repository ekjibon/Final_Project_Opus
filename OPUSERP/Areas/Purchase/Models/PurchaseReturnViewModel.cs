using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Purchase.Models
{
    public class PurchaseReturnViewModel
    {
        public int? customerId { get; set; }
        public int? invoiceId { get; set; }

        public int?[] selectPaymentHeadIds { get; set; }
        public int?[] selectPaymentHeadAmounts { get; set; }

        public DateTime? poDate { get; set; }
        public decimal? netTotal { get; set; }

        public IEnumerable<PurchaseReturnInfoMaster> purchaseReturnInfoMasters { get; set; }
        public IEnumerable<PurchaseReturnInfoDetail> purchaseReturnInfoDetails { get; set; }
        public PurchaseReturnInfoMaster purchaseReturnInfoMaster { get; set; }


        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentAttachment> photoes { get; set; }
        public IEnumerable<DocumentAttachment> documents { get; set; }

    }
}
