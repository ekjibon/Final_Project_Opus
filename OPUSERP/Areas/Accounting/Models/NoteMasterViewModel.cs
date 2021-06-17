using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity.AttachmentComment;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class NoteMasterViewModel
    {
        public int noteMasterId { get; set; }
        public int balanceSheetMasterId { get; set; }

        public string noteHead { get; set; }
        public string noteName { get; set; }
        public int? nmSerialNo { get; set; }

        public IEnumerable<NoteMaster> noteMasters { get; set; }
        public IEnumerable<BalanceSheetMaster> balanceSheetMasters { get; set; }
    }
}
