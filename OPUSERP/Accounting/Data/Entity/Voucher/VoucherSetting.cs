using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.Voucher
{
    [Table("VoucherSetting", Schema = "ACCOUNT")]
    public class VoucherSetting : Base
    {
        public int companyId { get; set; }
        public Company company { get; set; }

        public int? voucherTypeId { get; set; }//1 for Auto 2 for manual
        public int? bankAccountLedgerId { get; set; }
        public LedgerRelation bankAccountLedger { get; set; }

        public int? cashAccountLedgerId { get; set; }
        public LedgerRelation cashAccountLedger { get; set; }
        public int? paymentModeId { get; set; }
        public PaymentMode paymentMode { get; set; }
    }
}
