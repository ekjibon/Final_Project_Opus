using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class VoucherApproveLogViewModel
    {
        public int? voucherApproveLogId { get; set; }
        public int? voucherMasterId { get; set; }
        public string remarks { get; set; }
        public int? statusId { get; set; }

        public IEnumerable<VoucherMaster> voucherNotApproveLogs { get; set; }

       
    }
}
