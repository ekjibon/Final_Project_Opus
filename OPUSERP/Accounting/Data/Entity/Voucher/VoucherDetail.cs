using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.Voucher
{
    [Table("VoucherDetail", Schema = "ACCOUNT")]
    public class VoucherDetail : Base
    {
        public int? voucherId { get; set; }
        public VoucherMaster voucher { get; set; }

        public int ledgerRelationId { get; set; }
        public LedgerRelation ledgerRelation { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? amount { get; set; }

        public int? transectionModeId { get; set; }
        public TransectionMode transectionMode { get; set; }
        public int? isPrincAcc { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string accountName { get; set; }



        public int? specialBranchUnitId { get; set; }
        public SpecialBranchUnit specialBranchUnit { get; set; }
    }
}
