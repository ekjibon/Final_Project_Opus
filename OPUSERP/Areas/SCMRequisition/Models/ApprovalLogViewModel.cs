using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Areas.SCMRequisition.Models.Lang;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMRequisition.Models
{
    public class ApprovalLogViewModel
    {
        public int Id { get; set; }
        public int reqMasterId { get; set; }
        public string reqNo { get; set; }
        public DateTime? reqDate { get; set; }
        public string targetDate { get; set; }
        public string justification { get; set; }
        public string subject { get; set; }
        public string reqDept { get; set; }
        public int? projectId { get; set; }
        public string reqType { get; set; }
        public int? isCompWaiver { get; set; }
        public int? supplierId { get; set; }
        public string isPostPR { get; set; }
        public int? statusInfoId { get; set; }
        public int? reqBy { get; set; }

        public string itemName { get; set; }
        public string itemCode { get; set; }
        public string itemDescription { get; set; }

        public IEnumerable<RequisitionMaster> requisitionMastersList { get; set; }
        public IEnumerable<RequisitionDetail> requisitionDetailsList { get; set; }
        public IEnumerable<ApproverPanelViewModel> approerPanelList { get; set; }
        public IEnumerable<ApproverPanelFViewModel> approverPanelFViewModels { get; set; }
        public IEnumerable<RequisitionDetailVM> requisitionDetailVMs { get; set; }
        public ApprovalLog approvalLog { get; set; }
        public RequisitionApproveLN fLang { get; set; }
        public IEnumerable<GetRequisitionListForApprovedViewModel> getRequisitionListForApprovedViewModels { get; set; }
        public IEnumerable<DocumentAttachmentDetail> documentAttachmentDetails { get; set; }
    }
}
