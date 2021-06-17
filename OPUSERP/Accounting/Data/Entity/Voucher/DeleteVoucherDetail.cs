using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.Voucher
{
    [Table("DeleteVoucherDetail", Schema = "ACCOUNT")]
    public class DeleteVoucherDetail : Base
    {        

        public int? voucherDetailId { get; set; }

        public int? voucherMasterId { get; set; }

        public int? ledgerRelationId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? amount { get; set; }

        public int? transectionModeId { get; set; }
        public int? isPrincAcc { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string accountName { get; set; }
    }
}
