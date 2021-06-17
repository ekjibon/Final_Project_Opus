using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity.AttachmentComment;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class BalanceSheetDetailViewModel
    {
        public int balanceSheetDetailId { get; set; }
        public int? noteMasterId { get; set; }

        public int? ledgerId { get; set; }



        public IEnumerable<BalanceSheetMaster> balanceSheetMasters { get; set; }
        public IEnumerable<NoteMaster> noteMasters { get; set; }
        public IEnumerable<BalanceSheetDetails> balanceSheetDetails { get; set; }
        public BalanceSheetDetails balanceSheetDetail { get; set; }
        public IEnumerable<Ledger> ledgers { get; set; }
    }
}
