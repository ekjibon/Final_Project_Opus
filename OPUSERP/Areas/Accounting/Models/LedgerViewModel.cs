using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class LedgerViewModel
    {
        public int? groupId { get; set; }
        public int ledgerId { get; set; }
        public int subLedgerId { get; set; }        
        public string accountCode { get; set; }        
        public string accountName { get; set; }        
        public string aliasName { get; set; }
        public int? currencyId { get; set; }
        public int? haveSubLedger { get; set; }
        public int? specialBranchUnitId { get; set; }
        public string ledgerType { get; set; }
        public DateTime? effectiveDate { get; set; }

        public int?[] ids { get; set; }

        public IEnumerable<Ledger> ledgers { get; set; }

        public IEnumerable<AccountGroup> accountGroups { get; set; }
        public IEnumerable<SpecialBranchUnit> specialBranchUnits { get; set; }

        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public Ledger getLedgerDetailsById { get; set; }
    }
}
