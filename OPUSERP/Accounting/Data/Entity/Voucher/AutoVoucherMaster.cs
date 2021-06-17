using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Accounting.Data.Entity.Voucher
{
    [Table("AutoVoucherMaster", Schema = "ACCOUNT")]
    public class AutoVoucherMaster : Base
    {
        public int? autoVoucherNameId { get; set; }
        public AutoVoucherName autoVoucherName { get; set; }

        public int? voucherTypeId { get; set; }
        public VoucherType voucherType { get; set; }

        public int? projectId { get; set; }
        public Project project { get; set; }

        [MaxLength(500)]
        public string description { get; set; }
    }
}
