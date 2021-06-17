using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("LedgerRelation", Schema = "ACCOUNT")]
    public class LedgerRelation : Base
    {
        public int? ledgerId { get; set; }
        public Ledger ledger { get; set; }

        public int? subLedgerId { get; set; }
        public Ledger subLedger { get; set; }

        [NotMapped]
        public string accountCode { get; set; }
        [NotMapped]
        public string accountName { get; set; }
        [NotMapped]
        public int? haveSubLedger { get; set; }
        [NotMapped]
        public int? natureId { get; set; }
    }
}
