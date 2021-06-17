
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Purchase.Models
{
    public class PurchaseViewModel
    {
        
        public int? purchaseOrderMasterId { get; set; }
        public string poNo { get; set; }
        public DateTime? poDate { get; set; }
        public DateTime? paymentDate { get; set; }
        public int? relSupplierCustomerResourseId { get; set; }
        public DateTime? deliveryDate { get; set; }
        public int? deliveryTypeId { get; set; }
        public int? paymentTypeId { get; set; }
        public string rfqRef { get; set; }
        public string remarks { get; set; }
        public string billToLocation { get; set; }
        public int? saveStatusId { get; set; }

        public decimal? taxPercent { get; set; }
        public decimal? vatPercent { get; set; }
        public decimal? totalAmount { get; set; }
        public decimal? taxAmount { get; set; }
        public decimal? vatAmount { get; set; }
        public decimal? netTotal { get; set; }
        
        public int[] itemId { get; set; }

        public string[] itemSpecification { get; set; }

        public decimal?[] quantity { get; set; }

        public decimal?[] rate { get; set; }

       
        public IEnumerable<PurchaseOrderDetails>  purchaseOrderDetails { get; set; }
        public IEnumerable<PurchaseOrderMaster> purchaseOrderMasters { get; set; }
        public IEnumerable<SpecificationCategory> specificationCategories { get; set; }

        public PurchaseOrderMaster GetPurchaseOrderMasterById { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentAttachment> photoes { get; set; }
        public IEnumerable<DocumentAttachment> documents { get; set; }
        public IEnumerable<Company> companies { get; set; }

    }
}
