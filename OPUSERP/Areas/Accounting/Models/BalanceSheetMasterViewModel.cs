using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity.AttachmentComment;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class BalanceSheetMasterViewModel
    {
        public int balanceSheetMasterId { get; set; }
        public int? accountGroupId { get; set; }

        public string noteNo { get; set; }
        public int? serialNo { get; set; }

        public string fsLineName { get; set; }
        public string fsLineFor { get; set; }


        public IEnumerable<GroupNature> groupNatures { get; set; }
        public IEnumerable<AccountGroup> accountGroups { get; set; }

        public IEnumerable<BalanceSheetMaster> balanceSheetMasters { get; set; }
    }
}
