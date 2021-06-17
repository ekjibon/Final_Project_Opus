using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.Voucher
{
    [Table("VoucherApproveLog", Schema = "ACCOUNT")]
    public class VoucherApproveLog : Base
    {
        public int? voucherMasterId { get; set; }
        //public VoucherMaster voucherMaster { get; set; }
     
        public string remarks { get; set; }
        public int? statusId { get; set; }  //1=Entry and 2=approve and 3= reject and 4=Delete and 5=Re-Back
    }
}
