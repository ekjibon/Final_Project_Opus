using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("SubLedger", Schema = "ACCOUNT")]
    public class SubLedger : Base
    {
        public int? ledgerId { get; set; }
        public Ledger ledger { get; set; }

        public string accountCode { get; set; }

        public string accountName { get; set; }

        public string aliasName { get; set; }

        public int? isActive { get; set; }
    }
}
