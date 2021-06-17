using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.POS.Data.Entity;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Purchase.Models
{
    public class TransferViewModel
    {
        public int? transferId { get; set; }
        public DateTime? transferDate { get; set; }
        public string transferNO { get; set; }
        public int? storeFromId { get; set; }
        public int? storeToId { get; set; }
        public string remarks { get; set; }

        public int?[] itemSpecification { get; set; }
        public decimal?[] quantity { get; set; }
        //public decimal?[] rate { get; set; }


        public IEnumerable<TransferMaster> transferMasters { get; set; }
        public IEnumerable<TransferDetail> GetTransferDetailbyMasterId { get; set; }
        public TransferMaster GetTransferMasterByMasterId { get; set; }
        public IQueryable<TransferDetail> GetTransferDetailByMasterId { get; set; }

        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public IEnumerable<Store> stores { get; set; }
        public IEnumerable<StoreAssign> storeAssigns { get; set; }
    }
}
