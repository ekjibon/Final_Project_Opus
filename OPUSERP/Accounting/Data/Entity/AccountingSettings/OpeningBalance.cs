using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.AccountingSettings
{
    [Table("OpeningBalance", Schema = "ACCOUNT")]
    public class OpeningBalance : Base
    {
        public int? ledgerRelationId { get; set; }
        public LedgerRelation ledgerRelation { get; set; }
        public decimal? amount { get; set; }
        public int? transectionModeId { get; set; }
        public TransectionMode transectionMode { get; set; }
        public DateTime? balanceUpTo { get; set; }
    }
}
