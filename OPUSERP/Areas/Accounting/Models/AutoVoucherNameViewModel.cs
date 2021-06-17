using OPUSERP.Accounting.Data.Entity.Voucher;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class AutoVoucherNameViewModel
    {
        public int nameId { get; set; }
        public string autoVoucherName { get; set; }
        public string shortName { get; set; }

        public IEnumerable<AutoVoucherName> autoVoucherNames { get; set; }
    }
}
