using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.Voucher
{
    [Table("CostCentreAllocation", Schema = "ACCOUNT")]
    public class CostCentreAllocation : Base
    {
        public int? costCentreId { get; set; }
        public CostCentre costCentre { get; set; }

        public int? voucherDetailId { get; set; }
        public virtual VoucherDetail voucherDetail { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? amount { get; set; }
    }
}
