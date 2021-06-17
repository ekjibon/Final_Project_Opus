using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.Voucher
{
    [Table("DeleteVoucherMaster", Schema = "ACCOUNT")]
    public class DeleteVoucherMaster : Base
    {
        public int? voucherMasterId { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string voucherNo { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string refNo { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? voucherDate { get; set; }

        public int? voucherTypeId { get; set; }
        public virtual VoucherType voucherType { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string remarks { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? voucherAmount { get; set; }

        public int? isPosted { get; set; }

        public int? fiscalYearId { get; set; }

        public int? taxYearId { get; set; }

        public int? companyId { get; set; }

        public int? fundSourceId { get; set; }

        public int? specialBranchUnitId { get; set; }

        [MaxLength(100)]
        public string deletedBy { get; set; }

        public DateTime? deletedAt { get; set; }
    }
}
