using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.Voucher
{
    [Table("DeleteCostCentreAllocation", Schema = "ACCOUNT")]
    public class DeleteCostCentreAllocation : Base
    {
        public int? costCentreId { get; set; }

        public int? voucherDetailId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? amount { get; set; }
    }
}
