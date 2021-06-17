using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Areas.Accounting.Models
{
    [NotMapped]
    public class VoucherMasterViewModel
    {
        public int? voucherId { get; set; }

        public string voucherNo { get; set; }

        public string refNo { get; set; }

        public DateTime? voucherDate { get; set; }

        public int? voucherTypeId { get; set; }
        
        public string remarks { get; set; }
        
        public decimal? voucherAmount { get; set; }

        public int? isPosted { get; set; }

        public int? fiscalYearId { get; set; }

        public int? taxYearId { get; set; }

        public int? companyId { get; set; }

        public int? projectId { get; set; }

        public int? fundSourceId { get; set; }

        public decimal?[] drAmount { get; set; }

        public decimal?[] crAmount { get; set; }

        public int?[] ledgerRelationId { get; set; }

        public int?[] transectionModeId { get; set; }

        public string[] subLedgerName { get; set; }

        public int?[] isPrincAcc { get; set; }
        public decimal?[] costAmountAll { get; set; }
        public int?[] costCentreallId { get; set; }
        public string[] accountCode { get; set; }
        public int?[] natureId { get; set; }

        public virtual VoucherMaster VoucherMaster { get; set; }

        public IEnumerable<VoucherMaster> GetVoucherMasters { get; set; }
        public IEnumerable<VoucherMaster> voucherLists { get; set; }
        public virtual IEnumerable<VoucherDetail> VoucherDetails { get; set; }

        public VoucherMaster GetGetvoucherMasterById { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public IEnumerable<VoucherDetail> GetVoucherDetailsByMasterId { get; set; }
        public VoucherDetail voucherDetailbyprinAcc { get; set; }
        public IEnumerable<CostCentre> costCentres { get; set; }
        public IEnumerable<CostCentreAllocation> costCentreAllocations { get; set; }
        public Company company { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public GetSignatureViewModel getSignatureViewModel { get; set; }
    }
}
