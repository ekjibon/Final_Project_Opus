using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Accounting.Models
{
    public class DailyBillPaymentViewModel
    {
        public decimal? VatAmount { get; set; }
        public decimal? VatPayableAmount { get; set; }
        public decimal? VatCurrentAmount { get; set; }
        public decimal? InvoiceValue { get; set; }
        public decimal? BankPayable { get; set; }
        public decimal? PaidTAX { get; set; }
        public decimal? BalanceAount { get; set; }
        public decimal? BaseCalculatePrice { get; set; }
        public decimal? BasePrice { get; set; }
    }
}
