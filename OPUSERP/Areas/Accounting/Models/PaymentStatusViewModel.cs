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
    //[NotMapped]
    public class PaymentStatusViewModel
    {
        public int? voucherId { get; set; }

        public string voucherNo { get; set; }

        public DateTime? voucherDate { get; set; }

        public int? voucherTypeId { get; set; }
        
        public string remarks { get; set; }
        
        public decimal? voucherAmount { get; set; }

        public virtual VoucherMaster VoucherMaster { get; set; }
        
        public virtual IEnumerable<VoucherDetail> VoucherDetails { get; set; }

        public VoucherMaster GetGetvoucherMasterById { get; set; }
        public Company company { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<VoucherMaster> voucherMasters { get; set; }
    }
}
