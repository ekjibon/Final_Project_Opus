using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class VoucherSettingViewModel
    {
        public int voucherSettingId { get; set; }
        public int? voucherTypeId { get; set; }//1 for Auto 2 for manual
        public int bankAccountLedgerId { get; set; }
        public int cashAccountLedgerId { get; set; }
        public int? paymentModeId { get; set; }
       

        public IEnumerable<PaymentMode> paymentModes { get; set; }
        public IEnumerable<VoucherSetting> voucherSettings { get; set; }

       
    }
}
