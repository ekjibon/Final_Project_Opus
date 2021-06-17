using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Distribution.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Distribution.Models
{
    public class RequisitionViewModel
    {
        public int? masterId { get; set; }
        public int? customerId { get; set; }


        // public decimal?[] tblQuantity { get; set; }
        public int?[] packageDetailIds { get; set; }
        public int?[] itemPriceFixingId { get; set; }
        public decimal?[] tblQuantity { get; set; }
        public decimal?[] vattotal { get; set; }
        public decimal?[] taxtotal { get; set; }
        public decimal?[] discountTotal { get; set; }
        public decimal?[] cdtotal { get; set; }
        public decimal?[] sdtotal { get; set; }
        public decimal?[] attotal { get; set; }
        public decimal?[] rdtotal { get; set; }
        public decimal?[] lineTotal { get; set; }

        public decimal? grossTotal { get; set; }
        public decimal? grossVAT { get; set; }
        public decimal? grossTAX { get; set; }
        public decimal? grossSD { get; set; }
        public decimal? grossCD { get; set; }
        public decimal? grossAT { get; set; }
        public decimal? grossRD { get; set; }
        public decimal? grossDiscount { get; set; }
        public decimal? netTotal { get; set; }
        public decimal? vds { get; set; }
        public DateTime? requisitionDate { get; set; }
      

        public string requisitionNo { get; set; }

       public int?[] itemspecIds { get; set; }


      

        public IEnumerable<RequisitionMaster> requisitionMasters { get; set; }
      
        public RequisitionMaster requisitionMaster{ get; set; }

        public IEnumerable<RequisitionDetail> requisitionDetails { get; set; }
        public IEnumerable<RequisitionDetail> requisitionDetail { get; set; }

        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public IEnumerable<RequisitionApprovalViewModel> requisitionApprovalViewModels { get; set; }
        public Company company { get; set; }
       
    }
}
