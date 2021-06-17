using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.Voucher
{
    [Table("AutoVoucherDetail", Schema = "ACCOUNT")]
    public class AutoVoucherDetail : Base
    {
        public int? autoVoucherMasterId { get; set; }
        public AutoVoucherMaster autoVoucherMaster { get; set; }        

        public int? transectionModeId { get; set; }
        public TransectionMode transectionMode { get; set; }

        public int? ledgerRelationId { get; set; }
        public LedgerRelation ledgerRelation { get; set; }

    }
}
